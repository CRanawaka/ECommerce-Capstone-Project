using System.Security.Claims;
using AutoMapper;
using ECommerce.Api.Dtos;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ECommerce.Api.Controllers
{
    [ApiController]
    [Route("api/cart")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly IShoppingCartRepository _cartRepo;
        private readonly IMapper _mapper;

        public CartController(IShoppingCartRepository cartRepo, IMapper mapper)
        {
            _cartRepo = cartRepo;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<ActionResult<ShoppingCartDto>> GetCart()
        {
            var cart = await _cartRepo.GetCartByUserIdAsync(GetUserId().ToString());
            if (cart == null) return Ok(new ShoppingCartDto { UserId = GetUserId().ToString() }); // Return empty cart

            return Ok(_mapper.Map<ShoppingCartDto>(cart));
        }

        [HttpPost("items")]
        public async Task<ActionResult> AddItemToCart(int productId, int quantity)
        {
            await _cartRepo.AddItemToCartAsync(GetUserId().ToString(), productId, quantity);
            return Ok();
        }

        [HttpPut("items/{productId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateCartItem(int productId, [FromQuery] int quantity)
        {
            await _cartRepo.UpdateItemQuantityAsync(GetUserId().ToString(), productId, quantity);
            return NoContent();
        }

        [HttpDelete("items/{productId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> RemoveCartItem(int productId)
        {
            await _cartRepo.RemoveItemFromCartAsync(GetUserId().ToString(), productId);
            return NoContent();
        }
    }
}