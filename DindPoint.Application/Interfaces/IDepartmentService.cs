namespace DindPoint.Application.Interfaces;
using DindPoint.Application.DTOs.Department;
public interface IDepartmentService
{
    Task<List<DepartmentDto>> GetAllAsync();
    Task<DepartmentDto?> GetByIdAsync(int id);
    Task<DepartmentDto> CreateAsync(DepartmentCreateDto dto);
    Task<DepartmentDto?> UpdateAsync(int id, DepartmentUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}