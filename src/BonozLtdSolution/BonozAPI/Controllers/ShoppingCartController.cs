namespace BonozAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCart _shoppingCart;
        private readonly IProduct _product;

        public ShoppingCartController(IShoppingCart shoppingCartRepository,
                                      IProduct productRepository)
        {
            _shoppingCart = shoppingCartRepository;
            _product = productRepository;
        }

        [HttpGet]
        [Route("{userId}/GetItems")]
        public async Task<ActionResult<IEnumerable<CartItemDTO>>> GetItems(int userId)
        {
            try
            {
                var cartItems = await _shoppingCart.GetItems(userId);
                if (cartItems == null)
                    return NoContent();

                var products = await _product.GetProducts();
                if (products == null)
                    throw new Exception("No products exist in the system");

                var cartItemsDto = cartItems.ConvertToDto(products);

                return Ok(cartItemsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartItemDTO>> GetItem(int id)
        {
            try
            {
                var cartItem = await this._shoppingCart.GetItem(id);
                if (cartItem == null)
                    return NotFound();
                
                var product = await _product.GetProduct(cartItem.ProductId);
                if (product == null)
                    return NotFound();
                
                var cartItemDto = cartItem.ConvertToDto(product);
                return Ok(cartItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("{userId:int}")]
        public async Task<ActionResult<CartItemDTO>> PostItem([FromBody] CartItemToAddDTO cartItemToAddDto, int userId)
        {
            try
            {
                var newCartItem = await _shoppingCart.AddItem(cartItemToAddDto, userId);
                if (newCartItem == null)
                    return NoContent();

                var product = await _product.GetProduct(newCartItem.ProductId);
                if (product == null)
                    throw new Exception($"Something went wrong when attempting to retrieve product (productId:({cartItemToAddDto.ProductId})");

                var newCartItemDto = newCartItem.ConvertToDto(product);

                return CreatedAtAction(nameof(GetItem), new { id = newCartItemDto.Id }, newCartItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CartItemDTO>> DeleteItem(int id)
        {
            try
            {
                var cartItem = await _shoppingCart.DeleteItem(id);
                if (cartItem == null)
                    return NotFound();

                var product = await this._product.GetProduct(cartItem.ProductId);
                if (product == null)
                    return NotFound();

                var cartItemDto = cartItem.ConvertToDto(product);
                return Ok(cartItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<CartItemDTO>> UpdateQty(int id, CartItemQtyUpdateDTO cartItemQtyUpdateDto)
        {
            try
            {
                var cartItem = await _shoppingCart.UpdateQty(id, cartItemQtyUpdateDto);
                if (cartItem == null)
                    return NotFound();

                var product = await _product.GetProduct(cartItem.ProductId);
                var cartItemDto = cartItem.ConvertToDto(product);

                return Ok(cartItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}