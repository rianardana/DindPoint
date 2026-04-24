namespace DindPoint.Application.DTOs.Department;
public record DepartmentUpdateDto(int Id, string DepartmentName, int? HeadOfDepartmentId, int? ParentDepartmentId, bool IsActive);