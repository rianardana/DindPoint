namespace DindPoint.Application.Services;
using AutoMapper;
using DindPoint.Application.DTOs.Role;
using DindPoint.Application.Interfaces;
using DindPoint.Domain.Entities;

public class RoleService : IRoleService
{
    private readonly IRepository<Role> _repository;
    private readonly IMapper _mapper;

    public RoleService(IRepository<Role> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<RoleDto>> GetAllAsync()
    {
        var roles = await _repository.GetAllAsync(orderBy: r => r.RoleName);
        return _mapper.Map<List<RoleDto>>(roles);
    }

    public async Task<RoleDto?> GetByIdAsync(int id)
    {
        var role = await _repository.GetByIdAsync(id);
        return role != null ? _mapper.Map<RoleDto>(role) : null;
    }

    public async Task<RoleDto> CreateAsync(RoleCreateDto dto)
    {
        var role = _mapper.Map<Role>(dto);
        role.CreatedAt = DateTime.UtcNow;
        role.IsActive = true;
        _repository.Add(role);
        await _repository.SaveChangesAsync();
        return _mapper.Map<RoleDto>(role);
    }

    public async Task<RoleDto?> UpdateAsync(int id, RoleUpdateDto dto)
    {
        var role = await _repository.GetByIdAsync(id);
        if (role == null) return null;

        _mapper.Map(dto, role);
        _repository.Update(role);
        await _repository.SaveChangesAsync();
        return _mapper.Map<RoleDto>(role);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var role = await _repository.GetByIdAsync(id);
        if (role == null) return false;

        role.IsActive = false;
        _repository.Update(role);
        await _repository.SaveChangesAsync();
        return true;
    }
}