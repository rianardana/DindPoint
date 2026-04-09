using System.Collections.Generic;

namespace DindPoint.Domain.Entities;

public partial class Department
{
    public int Id { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public int? HeadOfDepartmentId { get; set; }
    public int? ParentDepartmentId { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public virtual User? HeadOfDepartment { get; set; }
    public virtual Department? ParentDepartment { get; set; }
    public virtual ICollection<Department>? SubDepartments { get; set; }
    public virtual ICollection<User>? Users { get; set; }
}