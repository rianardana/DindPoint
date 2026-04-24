namespace DindPoint.Application.DTOs.Role;
public record RoleDto(int Id, string RoleName, string? Description, bool IsActive, DateTime CreatedAt);