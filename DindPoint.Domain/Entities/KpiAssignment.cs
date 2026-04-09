using System.Collections.Generic;

namespace DindPoint.Domain.Entities;

public partial class KpiAssignment
{
    public int Id { get; set; }
    public int TemplateId { get; set; }
    public int AssignedTo { get; set; }
    public int AssignedBy { get; set; }
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public string PeriodType { get; set; } = "Monthly";
    public decimal? TargetValue { get; set; }
    public string Status { get; set; } = "Active";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public virtual KpiTemplate? Template { get; set; }
    public virtual User? AssignedToUser { get; set; }
    public virtual User? AssignedByUser { get; set; }
    public virtual ICollection<KpiProgress>? KpiProgresses { get; set; }
}