namespace ECommerce.Api.Dtos
{
    public class ShoppingCartDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = default!;
        public List<CartItemDto> Items { get; set; } = [];
        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
    }
}
