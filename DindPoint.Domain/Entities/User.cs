using System.Collections.Generic;

namespace DindPoint.Domain.Entities;

public partial class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public int? DepartmentId { get; set; }
    public int? ReportsTo { get; set; }
    public int HierarchyLevel { get; set; } = 0;
    public int RoleId { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public virtual Role? Role { get; set; }
    public virtual Department? Department { get; set; }
    public virtual User? Supervisor { get; set; }
    public virtual ICollection<User>? Subordinates { get; set; }
    public virtual ICollection<KpiAssignment>? KpiAssignmentsAsAssignee { get; set; }
    public virtual ICollection<KpiAssignment>? KpiAssignmentsAsAssigner { get; set; }
    public virtual ICollection<KpiProgress>? KpiProgresses { get; set; }
    public virtual ICollection<KpiApproval>? KpiApprovals { get; set; }
}