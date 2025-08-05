using ECommerce.Core.Entities;

namespace ECommerce.Core.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart?> GetCartByUserIdAsync(string userId);
        Task AddItemToCartAsync(string userId, int productId, int quantity);

    }
}
