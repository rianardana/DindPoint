using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DindPoint.Application.DTOs.Role;
using DindPoint.Application.Interfaces;

namespace DindPoint.Web.Controllers;

public class RoleController : Controller
{
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;

    public RoleController(IRoleService roleService, IMapper mapper)
    {
        _roleService = roleService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var roles = await _roleService.GetAllAsync();
        return View(roles);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RoleCreateDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        
        try
        {
            await _roleService.CreateAsync(dto);
            TempData["SuccessMessage"] = "Role berhasil ditambahkan.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Gagal menambahkan role: " + ex.Message;
            return View(dto);
        }
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var role = await _roleService.GetByIdAsync(id);
        if (role == null) return NotFound();
        return View(_mapper.Map<RoleUpdateDto>(role));
    }

  [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, RoleUpdateDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        
        try
        {
            var result = await _roleService.UpdateAsync(id, dto);
            if (result == null) return NotFound();
            
            TempData["SuccessMessage"] = "Role berhasil diperbarui.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Gagal memperbarui role: " + ex.Message;
            return View(dto);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _roleService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Role berhasil dihapus.";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Gagal menghapus role: " + ex.Message;
        }
        return RedirectToAction(nameof(Index));
    }
}