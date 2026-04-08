using Microsoft.EntityFrameworkCore;
using DindPoint.Domain.Entities; 

namespace DindPoint.Infrastructure;

public partial class AppDbContext : DbContext
{
    public AppDbContext() { }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public virtual DbSet<User> Users { get; set; }

    // Design-time configuration (untuk migration)
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Design-time configuration
            var connectionString = @"Server=.\sqlexpress;Database=DindPoint;User Id=sa;Password=.*Locked24;TrustServerCertificate=True;MultipleActiveResultSets=true;";
            
            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("DindPoint.Infrastructure"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}