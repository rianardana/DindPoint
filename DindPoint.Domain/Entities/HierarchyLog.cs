namespace DindPoint.Domain.Entities;

public partial class HierarchyLog
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int? OldReportsTo { get; set; }
    public int? NewReportsTo { get; set; }
    public int OldHierarchyLevel { get; set; }
    public int NewHierarchyLevel { get; set; }
    public int ChangedBy { get; set; }
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    public string? Reason { get; set; }

    public virtual User? User { get; set; }
    public virtual User? ChangedByUser { get; set; }
}