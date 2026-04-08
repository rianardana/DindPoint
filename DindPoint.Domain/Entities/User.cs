using System;
using System.Collections.Generic;

namespace DindPoint.Domain.Entities;

public partial class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty; // Nanti di-hash di service layer
    public string EmployeeName { get; set; } = string.Empty;
    public string? Department { get; set; }
    public int RoleId { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    // Navigation properties (akan dikonfigurasi di OnModelCreating)
    //public virtual Role? Role { get; set; }
    //public virtual ICollection<KpiAssignment>? KpiAssignments { get; set; }
    //public virtual ICollection<KpiApproval>? KpiApprovals { get; set; }
}