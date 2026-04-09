using System.Collections.Generic;

namespace DindPoint.Domain.Entities;

public partial class KpiTemplate
{
    public int Id { get; set; }
    public string TemplateName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string TargetType { get; set; } = "Numeric";
    public string? TargetUnit { get; set; }
    public decimal? TargetValue { get; set; }
    public string PeriodType { get; set; } = "Monthly";
    public int CreatedBy { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public virtual User? CreatedByUser { get; set; }
    public virtual ICollection<KpiAssignment>? KpiAssignments { get; set; }
}