using BonozDomain.AppUser;
using BonozDomain.DTO.SalesDTO;

namespace BonozAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProduct _product;
        private readonly IOrder _order;
        public ProductsController(IProduct product, IOrder order)
        {
            _product = product;
            _order = order;
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductDTO productDTO)
        {
            try
            {
                if (productDTO == null)
                {
                    return BadRequest("No product found");
                }
                var product = productDTO.ConvertToEntity();
                _product.CreateProduct(product);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(ProductDTO productDTO, int id)
        {
            try
            {
                if (productDTO == null)
                    return BadRequest("Product not found");

                var hasProduct = IsExistProduct(id);

                if (hasProduct != false)
                {
                    var product = productDTO.ConvertToEntity();
                    _product.UpdateProduct(product);
                    return Ok();
                }
                else
                {
                    return BadRequest("Product not found in database");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetItems()
        {
            try
            {
                var products = await _product.GetProducts();

                if (products == null)
                {
                    return NotFound();
                }
                else
                {
                    var productDtos = products.ConvertToDto();

                    return Ok(productDtos);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTO>> GetItem(int id)
        {
            try
            {
                var product = await _product.GetProduct(id);

                if (product == null)
                {
                    return NotFound();
                }
                else
                {
                    var productDto = product.ConvertToDto();
                    return Ok(productDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpGet("GetProductCategories")]
        public async Task<ActionResult<IEnumerable<ProductCategoryDTO>>> GetProductCategories()
        {
            try
            {
                var productCategories = await _product.GetCategories();
                var productCategoryDtos = productCategories.ConvertToDto();

                return Ok(productCategoryDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpGet("GetShops")]
        public async Task<ActionResult<IEnumerable<Shop>>> GetShops()
        {
            try
            {
                var shop = await _product.GetShops();
                return Ok(shop);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route("{categoryId}/GetItemsByCategory")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetItemsByCategory(int categoryId)
        {
            try
            {
                var products = await _product.GetItemsByProductCategory(categoryId);
                var productDtos = products.ConvertToDto();

                return Ok(productDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductCategory(int id)
        {
            try
            {
                if (IsExistProduct(id))
                {
                    await _product.DeleteProduct(id);
                    return Ok();
                }
                else
                {
                    return NotFound("Product not found in database");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }


        private bool IsExistProduct(int id)
        {
            var data = _product.GetProduct(id);
            if (data == null)
                return false;
            else
                return true;
        }
    }
}
