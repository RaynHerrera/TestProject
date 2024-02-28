using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductManagementApp.Models;

namespace ProductManagementApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly List<Product> _products;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
            _products = InMemoryDatabase.Products;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            _logger.LogInformation("Retrieving all products.");
            return _products;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _products.Find(p => p.Id == id);
            if (product == null)
            {
                _logger.LogWarning($"Product with ID {id} not found.");
                return NotFound();
            }
            _logger.LogInformation($"Retrieving product with ID {id}.");
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            _products.Add(product);
            _logger.LogInformation($"Product added successfully. ID: {product.Id}");
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Product product)
        {
            var index = _products.FindIndex(p => p.Id == id);
            if (index == -1)
            {
                _logger.LogWarning($"Product with ID {id} not found.");
                return NotFound();
            }
            _products[index] = product;
            _logger.LogInformation($"Product updated successfully. ID: {id}");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var index = _products.FindIndex(p => p.Id == id);
            if (index == -1)
            {
                _logger.LogWarning($"Product with ID {id} not found.");
                return NotFound();
            }
            _products.RemoveAt(index);
            _logger.LogInformation($"Product deleted successfully. ID: {id}");
            return NoContent();
        }
    }
}
