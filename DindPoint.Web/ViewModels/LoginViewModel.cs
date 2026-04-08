using System.ComponentModel.DataAnnotations;

namespace DindPoint.Web.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Username atau Email wajib diisi")]
    public string UserNameOrEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password wajib diisi")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; }
    public string? ReturnUrl { get; set; }
    public string? ErrorMessage { get; set; }
}