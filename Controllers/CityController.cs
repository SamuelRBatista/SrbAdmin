using Microsoft.AspNetCore.Mvc;
using SrbComercialAdmin.Services;

namespace SrbComercialAdmin.Controllers;

public class CityController : Controller
{
    private readonly CityService _cityService;

    public CityController(CityService cityService)
    {
       _cityService = cityService;  
    }
    public async Task<IActionResult> Index()
    {
        var cities = await _cityService.GetCitiesAsync();
        return View();
    }
}
