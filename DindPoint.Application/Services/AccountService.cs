using DindPoint.Application.DTOs.Auth;
using DindPoint.Application.Interfaces;
using DindPoint.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace DindPoint.Application.Services;

public class AccountService : IAccountService
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Role> _roleRepository;

    public AccountService(IRepository<User> userRepository, IRepository<Role> roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<LoginResult> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.Table()
            .Include(u => u.Role)
            .Include(u => u.Department)
            .FirstOrDefaultAsync(u => 
                (u.UserName == dto.UserName || u.Email == dto.UserName) 
                && u.IsActive);

        if (user == null)
            return LoginResult.Failed("Username atau email tidak ditemukan");

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            return LoginResult.Failed("Password salah");

        return LoginResult.SuccessResult(
            user.Id,
            user.UserName,
            user.EmployeeName,
            user.Role?.RoleName,
            user.DepartmentId,
            user.HierarchyLevel,
            dto.ReturnUrl);
    }

    public Task LogoutAsync(int userId)
    {
        return Task.CompletedTask;
    }
}