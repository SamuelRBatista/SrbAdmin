using Microsoft.AspNetCore.Mvc;
using SrbComercialAdmin.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;
using SrbComercialAdmin.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SrbComercialAdmin.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;

        public ProductController(ProductService productService, CategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;

        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductAsync();
            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
           
            await _productService.CreateProductAsync(product);
                return RedirectToAction(nameof(Index));
      
            return View(product);
        }


        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var categories = await _categoryService.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var products = await _productService.GetProductAsync();
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
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

                return View(product); // Retorna a view com os erros de validação para o usuário
            }

            try
            {
                await _productService.UpdateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao atualizar o produto: " + ex.Message);
                return View(product);
            }
        }


        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var products = await _productService.GetProductAsync();
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var categories = await _categoryService.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
