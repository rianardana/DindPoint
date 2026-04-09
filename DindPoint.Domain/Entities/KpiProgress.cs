namespace DindPoint.Domain.Entities;

public partial class KpiProgress
{
    public int Id { get; set; }
    public int AssignmentId { get; set; }
    public int UserId { get; set; }
    public int? SubmittedTo { get; set; }
    public decimal? ActualValue { get; set; }
    public string? EvidenceUrl { get; set; }
    public string? Notes { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ReviewedAt { get; set; }

    public virtual KpiAssignment? Assignment { get; set; }
    public virtual User? User { get; set; }
    public virtual User? Approver { get; set; }
    public virtual KpiApproval? KpiApproval { get; set; }
}