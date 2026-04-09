namespace DindPoint.Domain.Entities;

public partial class KpiApproval
{
    public int Id { get; set; }
    public int ProgressId { get; set; }
    public int ApproverId { get; set; }
    public bool IsApproved { get; set; }
    public string? Remarks { get; set; }
    public DateTime ApprovedAt { get; set; } = DateTime.UtcNow;

    public virtual KpiProgress? Progress { get; set; }
    public virtual User? Approver { get; set; }
}