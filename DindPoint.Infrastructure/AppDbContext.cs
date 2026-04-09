using Microsoft.EntityFrameworkCore;
using DindPoint.Domain.Entities;

namespace DindPoint.Infrastructure;

public partial class AppDbContext : DbContext
{
    public AppDbContext() { }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<KpiTemplate> KpiTemplates { get; set; }
    public virtual DbSet<KpiAssignment> KpiAssignments { get; set; }
    public virtual DbSet<KpiProgress> KpiProgresses { get; set; }
    public virtual DbSet<KpiApproval> KpiApprovals { get; set; }
    public virtual DbSet<HierarchyLog> HierarchyLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = @"Server=.\sqlexpress;Database=DindPoint;User Id=sa;Password=.*Locked24;TrustServerCertificate=True;MultipleActiveResultSets=true;";
            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("DindPoint.Infrastructure"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.RoleName).HasMaxLength(50).IsRequired();
            entity.HasData(
                new Role { Id = 1, RoleName = "Staff", Description = "Karyawan biasa" },
                new Role { Id = 2, RoleName = "Superior", Description = "Manager level" },
                new Role { Id = 3, RoleName = "HOD", Description = "Head of Department" }
            );
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DepartmentName).HasMaxLength(100).IsRequired();
            entity.HasOne(d => d.HeadOfDepartment)
                .WithMany()
                .HasForeignKey(d => d.HeadOfDepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(d => d.ParentDepartment)
                .WithMany(p => p.SubDepartments)
                .HasForeignKey(d => d.ParentDepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UserName).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Password).HasMaxLength(255).IsRequired();
            entity.Property(e => e.EmployeeName).HasMaxLength(100);
            entity.HasIndex(e => e.UserName).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasOne(d => d.Role)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(d => d.Department)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(d => d.Supervisor)
                .WithMany(p => p.Subordinates)
                .HasForeignKey(d => d.ReportsTo)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<KpiTemplate>(entity =>
        {
            entity.ToTable("KpiTemplate");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TemplateName).HasMaxLength(200).IsRequired();
            entity.Property(e => e.TargetType).HasMaxLength(50);
            entity.Property(e => e.TargetUnit).HasMaxLength(50);
            entity.Property(e => e.PeriodType).HasMaxLength(50);
            entity.HasOne(d => d.CreatedByUser)
                .WithMany()
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
        });

            modelBuilder.Entity<KpiAssignment>(entity =>
        {
            entity.ToTable("KpiAssignment");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PeriodType).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            
            entity.HasOne(d => d.Template)
                .WithMany(p => p.KpiAssignments)
                .HasForeignKey(d => d.TemplateId)
                .OnDelete(DeleteBehavior.Restrict);
            
            
            entity.HasOne(d => d.AssignedToUser)
                .WithMany(p => p.KpiAssignmentsAsAssignee)  
                .HasForeignKey(d => d.AssignedTo)
                .OnDelete(DeleteBehavior.Restrict);
            
            
            entity.HasOne(d => d.AssignedByUser)
                .WithMany(p => p.KpiAssignmentsAsAssigner)  
                .HasForeignKey(d => d.AssignedBy)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<KpiProgress>(entity =>
        {
            entity.ToTable("KpiProgress");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.EvidenceUrl).HasMaxLength(500);
            entity.HasOne(d => d.Assignment)
                .WithMany(p => p.KpiProgresses)
                .HasForeignKey(d => d.AssignmentId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(d => d.User)
                .WithMany(p => p.KpiProgresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(d => d.Approver)
                .WithMany()
                .HasForeignKey(d => d.SubmittedTo)
                .OnDelete(DeleteBehavior.SetNull);
        });

       modelBuilder.Entity<KpiApproval>(entity =>
        {
            entity.ToTable("KpiApproval");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Remarks).HasMaxLength(500);
            
            
            entity.HasOne(d => d.Progress)
                .WithOne(p => p.KpiApproval) 
                .HasForeignKey<KpiApproval>(d => d.ProgressId)  
                .OnDelete(DeleteBehavior.Cascade);
            
            
            entity.HasOne(d => d.Approver)
                .WithMany(p => p.KpiApprovals)  
                .HasForeignKey(d => d.ApproverId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<HierarchyLog>(entity =>
        {
            entity.ToTable("HierarchyLog");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(d => d.ChangedByUser)
                .WithMany()
                .HasForeignKey(d => d.ChangedBy)
                .OnDelete(DeleteBehavior.Restrict);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}