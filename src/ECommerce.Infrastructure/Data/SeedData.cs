using ECommerce.Core.Entities;
using Microsoft.Extensions.Logging;

namespace ECommerce.Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task SeedAsync(ECommerceContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Products.Any())
                {
                    var products = new List<Product>
                    {
                    new() { Name = "Laptop Pro", Description = "High-end laptop for professionals", Price = 1499.99m, StockQuantity = 50 },
                    new() { Name = "Wireless Mouse", Description = "Ergonomic wireless mouse", Price = 49.99m, StockQuantity = 200 },
                    new() { Name = "Mechanical Keyboard", Description = "RGB mechanical keyboard", Price = 129.99m, StockQuantity = 100 }
                    };
                    await context.Products.AddRangeAsync(products);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger("SeedData");
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }
    }
}
