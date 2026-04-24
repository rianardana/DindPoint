namespace DindPoint.Application.DTOs.Department;
public record DepartmentDto(int Id, string DepartmentName, int? HeadOfDepartmentId, int? ParentDepartmentId, bool IsActive, DateTime CreatedAt);