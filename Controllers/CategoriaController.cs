using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace SrbComercialAdmin.Controllers;

public class CategoriaController : Controller
{
    private readonly CategoryService _categoryService;
    public CategoriaController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name");

        return View();
    }
}
