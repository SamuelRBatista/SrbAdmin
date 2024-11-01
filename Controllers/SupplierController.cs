using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using SrbComercialAdmin.Services;
using Org.BouncyCastle.Crypto;
using SrbComercialAdmin.Models;
using MySqlX.XDevAPI;


namespace SrbComercialAdmin.Controllers;

public class SupplierController : Controller
{
    private readonly SupplierService _supplierService;
    private readonly CityService _cityService;
    private readonly StateService _stateService;

    public SupplierController(SupplierService supplierService, CityService cityService, StateService stateService)
    {
       _supplierService = supplierService;
       _cityService = cityService;
       _stateService = stateService;   
    }
    public async Task<IActionResult> Index()
    {  
        var supplier = await _supplierService.GetSupplierAsync();
        return View(supplier);
    }

    public async Task<IActionResult> Create()
    {
        var states = await _stateService.GetStatesAsync();
        ViewBag.States = new SelectList(states, "Id", "Name");

        var cities = await _cityService.GetCitiesAsync();
        ViewBag.Cities = new SelectList(cities, "Id", "Name");         

        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Supplier supplier)
    {
        await _supplierService.CreateSupplierAsync(supplier);
        return RedirectToAction(nameof(Index));

        return View();  
    }

    public async Task<IActionResult> Edit(int id)
    {
        var states = await _stateService.GetStatesAsync();
        ViewBag.States = new SelectList(states, "Id", "Name");

        var cities = await _cityService.GetCitiesAsync();
        ViewBag.Cities = new SelectList(cities, "Id", "Name");

        var supplier = await _supplierService.GetSupplierAsync();
        var suppliers = supplier.FirstOrDefault(s => s.Id == id);

        if (suppliers == null)
        {
            return NotFound();
        }

        return View(suppliers);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Supplier supplier)
    {
        if(id != supplier.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            foreach(var error in ModelState)
            {
                foreach(var subError in error.Value.Errors)
                {
                    Console.WriteLine($"Erro no campo {error.Key}: {subError.ErrorMessage}");
                }

                return View(supplier);
            }
        }

        try
        {
            await _supplierService.UpdateSupplierAsync(supplier);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Erro ao atualizar o fornecedor: " + ex.Message);
            return View(supplier);
        }

    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirm(int id)
    {
        var states = await _stateService.GetStatesAsync();
        ViewBag.States = new SelectList(states, "Id", "Name");

        var cities = await _cityService.GetCitiesAsync();
        ViewBag.Cities = new SelectList(cities, "Id", "Name");

        await _supplierService.DeleteSupplierAsync(id);
        return RedirectToAction(nameof(Index));
    }

}
