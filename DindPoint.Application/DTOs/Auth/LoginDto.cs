namespace DindPoint.Application.DTOs.Auth;

public record LoginDto
{
    public string UserName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string? ReturnUrl { get; init; }
}