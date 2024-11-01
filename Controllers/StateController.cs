using Microsoft.AspNetCore.Mvc;

namespace SrbComercialAdmin.Controllers;

public class StateController : Controller
{
    private readonly StateService _stateService;

    public StateController(StateService stateService)
    {
        _stateService = stateService;    
    }
    public async Task<IActionResult> Index()
    {
        var states = await _stateService.GetStatesAsync();
        return View();
    }
}
