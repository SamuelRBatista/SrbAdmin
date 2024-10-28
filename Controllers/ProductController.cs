using Microsoft.AspNetCore.Mvc;
using SrbComercialAdmin.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;

namespace SrbComercialAdmin.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var token = "CfDJ8BiULaTaI_BFjiHkYd9heyG-59_T9jH6njVS7851GF_iMYjuXWhFvJBvbVn3E9wg4hETlGsop089lvlB92o4esmONhCCjgib05n8EX8Bj4UKFL2wkHDHqF7I6kr37JSsGhkBZyGqHmS_np5aTcy93nsjgF4c07OdZV61z-f5Ud5f543t_WAQF2VRPhCXMZEN1LNFluEM-MBZYTk8qLeNq68gf5vo1_wi0cpljkQdfgBUtlv3u122hUo89B1ggTe1DOyjirVw0t9UtigRGFc5KmxsSH-3W9urIJdtABDLsRrnrThPjPD9gLzs1HlumNQ_flHipghVHEAueN285JBxqGopFavL1ytczVx6GUwolmjIhPMASPcRC57_f0HfefU5g6ngixkV8FiqNpB6uzOYkh_btTMIyWhlbyRnlYl863Fheut-hGHMrkLWK2MDha2Kgp_u0HS1odijoyIYJRvRD1RpxRD2B9vFh88nS-IZofzlTFcaS5lb2D42cEyMfRBv-iumlMatNt8Uq6DTNadI6-Vl-m65fzInaCgOVVNkueF-6onFWImte_FCb8iypN6bunhslEGc4BnR5PXd0mNw8IxbXH5YFVonR1AhjrcVzNnKBc021BiparL3UUDgbWA2vY-HCIea5A7ba4fgaiB2aHaBbRSC4nyUxLGO98y0Gd1-bUeGPs8d7d7zVKTwRbg0gynj2NbtMuJR3_fF65oOzmg"; // Substitua pelo seu token de autenticação
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var response = await _httpClient.GetAsync("https://localhost:7056/api/Product");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var products = JsonSerializer.Deserialize<IEnumerable<Product>>(content);
                    Console.WriteLine(products);
                    return View(products);
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Erro: {response.StatusCode} - {errorMessage}");
                    return RedirectToAction("Error", "Home");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao buscar produtos: {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product); // Retornar a view com o produto caso o modelo não seja válido
            }

            var token = "CfDJ8BiULaTaI_BFjiHkYd9heyG-59_T9jH6njVS7851GF_iMYjuXWhFvJBvbVn3E9wg4hETlGsop089lvlB92o4esmONhCCjgib05n8EX8Bj4UKFL2wkHDHqF7I6kr37JSsGhkBZyGqHmS_np5aTcy93nsjgF4c07OdZV61z-f5Ud5f543t_WAQF2VRPhCXMZEN1LNFluEM-MBZYTk8qLeNq68gf5vo1_wi0cpljkQdfgBUtlv3u122hUo89B1ggTe1DOyjirVw0t9UtigRGFc5KmxsSH-3W9urIJdtABDLsRrnrThPjPD9gLzs1HlumNQ_flHipghVHEAueN285JBxqGopFavL1ytczVx6GUwolmjIhPMASPcRC57_f0HfefU5g6ngixkV8FiqNpB6uzOYkh_btTMIyWhlbyRnlYl863Fheut-hGHMrkLWK2MDha2Kgp_u0HS1odijoyIYJRvRD1RpxRD2B9vFh88nS-IZofzlTFcaS5lb2D42cEyMfRBv-iumlMatNt8Uq6DTNadI6-Vl-m65fzInaCgOVVNkueF-6onFWImte_FCb8iypN6bunhslEGc4BnR5PXd0mNw8IxbXH5YFVonR1AhjrcVzNnKBc021BiparL3UUDgbWA2vY-HCIea5A7ba4fgaiB2aHaBbRSC4nyUxLGO98y0Gd1-bUeGPs8d7d7zVKTwRbg0gynj2NbtMuJR3_fF65oOzmg"; // Substitua pelo seu token de autenticação
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var jsonContent = JsonSerializer.Serialize(product);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://localhost:7056/api/Product", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index"); // Redireciona para a lista de produtos após sucesso
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Erro: {response.StatusCode} - {errorMessage}");
                    return View(product);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao salvar o produto: {ex.Message}");
                return View(product);
            }
        }
    }
}
