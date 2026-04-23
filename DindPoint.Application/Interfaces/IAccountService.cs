using DindPoint.Application.DTOs.Auth;

namespace DindPoint.Application.Interfaces;

public interface IAccountService
{
    Task<LoginResult> LoginAsync(LoginDto dto);
    Task LogoutAsync(int userId);
}