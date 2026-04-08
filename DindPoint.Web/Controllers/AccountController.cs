using Microsoft.AspNetCore.Mvc;
using DindPoint.Application.Interfaces;
using DindPoint.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DindPoint.Web.ViewModels;
namespace DindPoint.Web.Controllers;

public class AccountController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Login(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
    return View(new LoginViewModel { ReturnUrl = returnUrl });
       
    }
}