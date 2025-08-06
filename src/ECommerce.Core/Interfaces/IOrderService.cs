using ECommerce.Core.Entities;

namespace ECommerce.Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderFromCartAsync(int userId);
    }
}