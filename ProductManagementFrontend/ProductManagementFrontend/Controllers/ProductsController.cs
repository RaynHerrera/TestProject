using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductManagementFrontend.Models;

namespace ProductManagementFrontend.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IHttpClientFactory clientFactory, ILogger<ProductsController> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var client = _clientFactory.CreateClient("API");
                var response = await client.GetAsync("/products");
                if (response.IsSuccessStatusCode)
                {
                    var products = await response.Content.ReadAsStringAsync();
                    var productList = JsonConvert.DeserializeObject<List<Product>>(products);
                    return View(productList);
                }
                else
                {
                    _logger.LogError($"Failed to retrieve products: {response.StatusCode}");
                    throw new Exception($"Failed to retrieve products: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching products: {ex.Message}");
                throw;
            }
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var client = _clientFactory.CreateClient("API");
                var response = await client.GetAsync($"/products/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var product = await response.Content.ReadAsStringAsync();
                    var productObj = JsonConvert.DeserializeObject<Product>(product);
                    if (productObj == null)
                    {
                        return NotFound();
                    }
                    return View(productObj);
                }
                else
                {
                    _logger.LogError($"Failed to retrieve product details: {response.StatusCode}");
                    throw new Exception($"Failed to retrieve product details: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching product details: {ex.Message}");
                throw;
            }
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var client = _clientFactory.CreateClient("API");
                    var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("/products", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        _logger.LogError($"Failed to create product: {response.StatusCode}");
                        throw new Exception($"Failed to create product: {response.StatusCode}");
                    }
                }
                return View(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while creating a new product: {ex.Message}");
                throw;
            }
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid) return BadRequest(ModelState);

                var client = _clientFactory.CreateClient("API");
                var response = await client.GetAsync($"/products/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var product = await response.Content.ReadAsStringAsync();
                    var productObj = JsonConvert.DeserializeObject<Product>(product);
                    if (productObj == null)
                    {
                        return NotFound();
                    }
                    return View(productObj);
                }
                else
                {
                    _logger.LogError($"Failed to edit product details: {response.StatusCode}");
                    throw new Exception($"Failed to edit product details: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while editing the product: {ex.Message}");
                throw;
            }
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price")] Product product)
        {
            try
            {
                if (id != product.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var client = _clientFactory.CreateClient("API");
                    var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                    var response = await client.PutAsync($"/products/{id}", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        _logger.LogError($"Failed to update product: {response.StatusCode}");
                        throw new Exception($"Failed to update product: {response.StatusCode}");
                    }
                }
                return View(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while editing a new product: {ex.Message}");
                throw;
            }
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var client = _clientFactory.CreateClient("API");
                var response = await client.GetAsync($"/products/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var product = await response.Content.ReadAsStringAsync();
                    var productObj = JsonConvert.DeserializeObject<Product>(product);
                    if (productObj == null)
                    {
                        return NotFound();
                    }
                    return View(productObj);
                }
                else
                {
                    _logger.LogError($"Failed to delete the selected product: {response.StatusCode}");
                    throw new Exception($"Failed to delete the selected product: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting the product: {ex.Message}");
                throw;
            }
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var client = _clientFactory.CreateClient("API");
                var response = await client.DeleteAsync($"/products/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogError($"Failed to delete product: {response.StatusCode}");
                    throw new Exception($"Failed to delete product: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting the product: {ex.Message}");
                throw;
            }
        }
    }
}
