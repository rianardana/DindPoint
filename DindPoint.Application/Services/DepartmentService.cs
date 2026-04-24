namespace DindPoint.Application.Services;
using AutoMapper;
using DindPoint.Application.DTOs.Department;
using DindPoint.Application.Interfaces;
using DindPoint.Domain.Entities;
public class DepartmentService : IDepartmentService
{
    private readonly IRepository<Department> _repository;
    private readonly IMapper _mapper;

    public DepartmentService(IRepository<Department> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<DepartmentDto>> GetAllAsync()
    {
        var depts = await _repository.GetAllAsync(orderBy: d => d.DepartmentName);
        return _mapper.Map<List<DepartmentDto>>(depts);
    }

    public async Task<DepartmentDto?> GetByIdAsync(int id)
    {
        var dept = await _repository.GetByIdAsync(id);
        return dept != null ? _mapper.Map<DepartmentDto>(dept) : null;
    }

    public async Task<DepartmentDto> CreateAsync(DepartmentCreateDto dto)
    {
        var dept = _mapper.Map<Department>(dto);
        dept.CreatedAt = DateTime.UtcNow;
        dept.IsActive = true;
        _repository.Add(dept);
        await _repository.SaveChangesAsync();
        return _mapper.Map<DepartmentDto>(dept);
    }

    public async Task<DepartmentDto?> UpdateAsync(int id, DepartmentUpdateDto dto)
    {
        var dept = await _repository.GetByIdAsync(id);
        if (dept == null) return null;

        _mapper.Map(dto, dept);
        dept.UpdatedAt = DateTime.UtcNow;
        _repository.Update(dept);
        await _repository.SaveChangesAsync();
        return _mapper.Map<DepartmentDto>(dept);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var dept = await _repository.GetByIdAsync(id);
        if (dept == null) return false;

        dept.IsActive = false;
        dept.UpdatedAt = DateTime.UtcNow;
        _repository.Update(dept);
        await _repository.SaveChangesAsync();
        return true;
    }
}