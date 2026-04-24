namespace DindPoint.Application.DTOs.Role;
public record RoleUpdateDto(int Id, string RoleName, string? Description, bool IsActive);