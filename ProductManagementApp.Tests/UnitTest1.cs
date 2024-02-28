using NUnit.Framework;
using ProductManagementApp.Controllers;
using ProductManagementApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace ProductManagementApp.Tests
{
    [TestFixture]
    public class ProductsControllerTests
    {
        private ProductsController _controller;
        private ILogger<ProductsController> _logger;
        private List<Product> _products;

        [SetUp]
        public void Setup()
        {
            _logger = new LoggerFactory().CreateLogger<ProductsController>();
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Description = "Description 1", Price = 10.99M },
                new Product { Id = 2, Name = "Product 2", Description = "Description 2", Price = 20.99M }
            };
            _controller = new ProductsController(_logger)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            _controller.HttpContext.Request.Headers["Authorization"] = "Bearer FakeToken"; // Optionally set authorization header
        }

        [Test]
        public void Get_ReturnsAllProducts()
        {
            // Arrange
            _controller = new ProductsController(_logger);

            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Product>>(result);
        }

        [Test]
        public void Post_AddsProduct()
        {
            // Arrange
            var newProduct = new Product { Id = 3, Name = "New Product", Description = "New Description", Price = 30.99M };

            // Act
            var result = _controller.Post(newProduct) as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual("Get", result.ActionName);
            Assert.AreEqual(newProduct, result.Value);
        }

        [Test]
        public void Put_UpdatesProduct()
        {
            // Arrange
            int id = 1;
            var updatedProduct = new Product { Id = id, Name = "Updated Product", Description = "Updated Description", Price = 15.99M };

            // Act
            var result = _controller.Put(id, updatedProduct) as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Delete_RemovesProduct()
        {
            // Arrange
            int id = 1;

            // Act
            var result = _controller.Delete(id) as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }
    }
}
