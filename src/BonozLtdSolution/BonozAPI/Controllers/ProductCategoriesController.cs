namespace BonozAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {

        private readonly IProduct _product;

        public ProductCategoriesController(IProduct product)
        {
            _product = product;
        }

        [HttpPost]
        public async Task<ActionResult> CreateProductCategory(ProductCategoryDTO category)
        {
            try
            {
                if (category != null)
                {
                    var categoryEntity = category.ConvertToEntity();
                    _product.CreateCategory(categoryEntity);

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProductCategory(ProductCategoryDTO category, int id)
        {
            try
            {
                if (category != null && IsExistCategory(id))
                {
                    var categoryEntity = category.ConvertToEntity();
                    _product.UpdateCategory(categoryEntity);

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IList<ProductCategory>>> GetProductCategories()
        {
            try
            {
                var productCategories = await _product.GetCategories();
                return Ok(productCategories);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategoryDTO>> GetProductCategoryById(int id)
        {
            try
            {
                var category = await _product.GetCategory(id);

                if (category == null)
                {
                    return NotFound();
                }
                else
                {
                    var categoryDto = category.ConvertToDto();

                    return Ok(categoryDto);
                }
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
                if (IsExistCategory(id))
                {
                    await _product.DeleteCategory(id);
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

        private bool IsExistCategory(int id)
        {
            var data = _product.GetCategory(id);
            if (data == null)
                return false;
            else
                return true;
        }
    }
}