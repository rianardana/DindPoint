using Microsoft.AspNetCore.Mvc;
using DindPoint.Application.Interfaces;
using DindPoint.Application.DTOs.Auth;
using DindPoint.Web.ViewModels;
using AutoMapper;

namespace DindPoint.Web.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public AccountController(IAccountService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }
        
        ViewBag.ReturnUrl = returnUrl;
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        var loginDto = _mapper.Map<LoginDto>(model);
        loginDto = loginDto with { ReturnUrl = returnUrl };

        var result = await _accountService.LoginAsync(loginDto);

        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        SetUserSession(result);
        return Redirect(result.RedirectUrl!);
    }

    private void SetUserSession(LoginResult result)
    {
        HttpContext.Session.SetInt32("UserId", result.UserId.Value);
        HttpContext.Session.SetString("UserName", result.UserName!);
        HttpContext.Session.SetString("EmployeeName", result.EmployeeName!);
        HttpContext.Session.SetString("UserRole", result.RoleName!);
        HttpContext.Session.SetInt32("HierarchyLevel", result.HierarchyLevel);
        
        if (result.DepartmentId.HasValue)
        {
            HttpContext.Session.SetInt32("DepartmentId", result.DepartmentId.Value);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        
        if (userId.HasValue)
        {
            await _accountService.LogoutAsync(userId.Value);
        }

        HttpContext.Session.Clear();
        
        return RedirectToAction(nameof(Login));
    }
}