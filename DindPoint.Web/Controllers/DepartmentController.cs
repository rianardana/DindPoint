namespace DindPoint.Web.Controllers;
using DindPoint.Application.DTOs.Department;
using DindPoint.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
public class DepartmentController : Controller
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
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
        await _departmentService.CreateAsync(dto);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var dept = await _departmentService.GetByIdAsync(id);
        if (dept == null) return NotFound();
        return View(dept);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, DepartmentUpdateDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        var result = await _departmentService.UpdateAsync(id, dto);
        if (result == null) return NotFound();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _departmentService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}