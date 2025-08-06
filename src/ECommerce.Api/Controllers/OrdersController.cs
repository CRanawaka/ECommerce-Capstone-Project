using System.Security.Claims;
using AutoMapper;
using ECommerce.Api.Dtos;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            var order = await _orderService.CreateOrderFromCartAsync(GetUserId());

            if (order == null)
            {
                return BadRequest(new { message = "Cannot create order. Your cart might be empty." });
            }

            return CreatedAtAction(nameof(CreateOrder), new { id = order.Id }, _mapper.Map<OrderDto>(order));
        }
    }
}