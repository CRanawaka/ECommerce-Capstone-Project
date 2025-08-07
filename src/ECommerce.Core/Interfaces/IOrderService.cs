using ECommerce.Core.Entities;

namespace ECommerce.Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderFromCartAsync(int userId);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(int userId);
        Task<Order?> GetOrderDetailsAsync(int orderId, int userId);
    }
}