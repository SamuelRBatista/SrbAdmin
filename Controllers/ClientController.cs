using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SrbComercialAdmin.Models;
using SrbComercialAdmin.Services;

namespace SrbComercialAdmin.Controllers
{
    public class ClientController : Controller
    {

        private readonly ClientService _clientService;
        private readonly StateService _stateService;
        private readonly CityService _cityService;

        public ClientController(ClientService clientService, StateService stateService, CityService cityService)
        {
            _clientService = clientService;
            _stateService = stateService;
            _cityService = cityService;

        }
          public async Task<IActionResult> Index()
          {
                var clients = await _clientService.GetClientAsync(); // Método que busca todos os clientes

                // Para cada cliente, obter o nome da cidade correspondente
                foreach (var client in clients)
                {
                    var city = await _cityService.GetCityByIdAsync(client.CityId);
                    client.CityName = city?.Name; 
                }

                    return View(clients);
          }

        public async Task<IActionResult> Create()
        {
            // Obter estados
            var states = await _stateService.GetStatesAsync();
            ViewBag.States = new SelectList(states, "Id", "Name");
           
            var cities = await _cityService.GetCitiesAsync();
            ViewBag.Cities = new SelectList(cities, "Id", "Name");

            return View(); // Passar um novo objeto Client para a view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client)
        {

            await _clientService.CreateClientAsync(client);
            return RedirectToAction(nameof(Index));

            return View(client);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var states = await _stateService.GetStatesAsync();
            ViewBag.States = new SelectList(states, "Id", "Name");

            var cities = await _cityService.GetCitiesAsync();
            ViewBag.Cities = new SelectList(cities, "Id", "Name");

            var client = await _clientService.GetClientAsync();
            var clients = client.FirstOrDefault(c => c.Id == id);

            if (clients == null)
            {
                return NotFound();
            }

            return View(clients);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                // Log de erros para depuração
                foreach (var error in ModelState)
                {
                    foreach (var subError in error.Value.Errors)
                    {
                        Console.WriteLine($"Erro no campo {error.Key}: {subError.ErrorMessage}");
                    }
                }

                return View(client); // Retorna a view com os erros de validação para o usuário
            }

            try
            {
                await _clientService.UpdateClientAsync(client);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao atualizar o client: " + ex.Message);
                return View(client);
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

            await _clientService.DeleteClientAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
