using System.Collections.Generic;

namespace ProductManagementApp.Models
{
    public static class InMemoryDatabase
    {
        public static List<Product> Products { get; } = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", Description = "Description 1", Price = 10.99M },
            new Product { Id = 2, Name = "Product 2", Description = "Description 2", Price = 20.99M },
        };
    }
}