using System.Collections.Generic;

namespace DindPoint.Domain.Entities;

public partial class Role
{
    public int Id { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<User>? Users { get; set; }
}