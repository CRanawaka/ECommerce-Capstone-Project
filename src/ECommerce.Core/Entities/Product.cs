using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Core.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;


    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }

    // Additional properties and methods can be added here as needed
}