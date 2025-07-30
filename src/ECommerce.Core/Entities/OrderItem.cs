namespace ECommerce.Core.Entities;

public class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!; // Navigation property to Order

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!; // Navigation property to Product

    public int Quantity { get; set; }
    public decimal Price { get; set; }

    // Additional properties and methods can be added here as needed
}
