using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> CreateCart()
        {
            var cart = new Cart();
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCart), new { id = cart.Id }, cart);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        [HttpPost("{cartId}/items")]
        public async Task<IActionResult> AddToCart(int cartId, [FromBody] CartItem cartItem)
        {
            if (cartItem == null || cartItem.ProductId <= 0 || cartItem.Quantity <= 0)
            {
                return BadRequest("Invalid cart item data");
            }

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart == null)
            {
                return NotFound("Cart not found");
            }

            var product = await _context.Products.FindAsync(cartItem.ProductId);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            var existingItem = cart.CartItems.FirstOrDefault(i => i.ProductId == cartItem.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += cartItem.Quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity
                });
            }

            await _context.SaveChangesAsync();
            return Ok(cart);
        }

        [HttpDelete("{cartId}/items/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int cartId, int productId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.Id == cartId);

            if (cart == null)
            {
                return NotFound("Cart not found");
            }

            var cartItem = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
            if (cartItem == null)
            {
                return NotFound("Item not found in cart");
            }

            cart.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}