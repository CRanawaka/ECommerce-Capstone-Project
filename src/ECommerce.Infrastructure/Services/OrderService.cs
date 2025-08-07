using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly ECommerceContext _context;
        private readonly IShoppingCartRepository _cartRepo;

        public OrderService(ECommerceContext context, IShoppingCartRepository cartRepo)
        {
            _context = context;
            _cartRepo = cartRepo;
        }

        public async Task<Order?> CreateOrderFromCartAsync(int userId)
        {
            var cart = await _cartRepo.GetCartByUserIdAsync(userId.ToString());
            if (cart == null || !cart.Items.Any())
            {
                // Cannot create an order from an empty cart
                return null;
            }

            var orderItems = new List<OrderItem>();
            foreach (var item in cart.Items)
            {
                // Create an OrderItem, locking in the price at the time of purchase
                orderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                });
            }

            var order = new Order
            {
                UserId = userId,
                OrderItems = orderItems,
                TotalAmount = orderItems.Sum(oi => oi.Price * oi.Quantity)
            };

            await _context.Orders.AddAsync(order);

            // Clear the user's shopping cart
            _context.CartItems.RemoveRange(cart.Items);

            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderDetailsAsync(int orderId, int userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);
        }
    }
}