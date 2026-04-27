using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DindPoint.Application.DTOs.Department;
using DindPoint.Application.Interfaces;

namespace DindPoint.Web.Controllers;

public class DepartmentController : Controller
{
    private readonly IDepartmentService _departmentService;
    private readonly IMapper _mapper;

    public DepartmentController(IDepartmentService departmentService, IMapper mapper)
    {
        _departmentService = departmentService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var departments = await _departmentService.GetAllAsync();
        return View(departments);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DepartmentCreateDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        
        try
        {
            await _departmentService.CreateAsync(dto);
            TempData["SuccessMessage"] = "Department berhasil ditambahkan.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Gagal menambahkan department: {ex.Message}";
            return View(dto);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var dept = await _departmentService.GetByIdAsync(id);
        if (dept == null) return NotFound();
        
        // Map ke UpdateDto biar sesuai dengan View Model
        return View(_mapper.Map<DepartmentUpdateDto>(dept));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, DepartmentUpdateDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        
        try
        {
            var result = await _departmentService.UpdateAsync(id, dto);
            if (result == null) return NotFound();
            
            TempData["SuccessMessage"] = "Department berhasil diperbarui.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Gagal memperbarui department: {ex.Message}";
            return View(dto);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _departmentService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Department berhasil dihapus.";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Gagal menghapus department: {ex.Message}";
        }
        return RedirectToAction(nameof(Index));
    }
}