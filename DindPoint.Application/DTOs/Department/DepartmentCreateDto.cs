namespace DindPoint.Application.DTOs.Department;
public record DepartmentCreateDto(string DepartmentName, int? HeadOfDepartmentId, int? ParentDepartmentId);