namespace DindPoint.Application.Interfaces;
using DindPoint.Application.DTOs.Role;
public interface IRoleService
{
    Task<List<RoleDto>> GetAllAsync();
    Task<RoleDto?> GetByIdAsync(int id);
    Task<RoleDto> CreateAsync(RoleCreateDto dto);
    Task<RoleDto?> UpdateAsync(int id, RoleUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}