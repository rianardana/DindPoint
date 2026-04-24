namespace DindPoint.Web.Controllers;
using DindPoint.Application.DTOs.Role;
using DindPoint.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
public class RoleController : Controller
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<IActionResult> Index()
    {
        var roles = await _roleService.GetAllAsync();
        return View(roles);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RoleCreateDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _roleService.CreateAsync(dto);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var role = await _roleService.GetByIdAsync(id);
        if (role == null) return NotFound();
        return View(role);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, RoleUpdateDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        var result = await _roleService.UpdateAsync(id, dto);
        if (result == null) return NotFound();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _roleService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}