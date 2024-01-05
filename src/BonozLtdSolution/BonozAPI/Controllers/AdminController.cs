namespace BonozAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IProduct _product;
        public AdminController(IProduct product)
        {
            _product = product;
        }

        //#region Product

        //[HttpGet("Product")]
        //public async Task<ActionResult<IList<Product>>> GetProducts()
        //{
        //    try
        //    {
        //        var products = _product.GetProducts();
        //        if (products == null)
        //            return NotFound("Product Not Found");
        //        else
        //            return Ok(products);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //                        "Error retrieving data from the database");
        //    }
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Product>> GetProduct(int id)
        //{
        //    try
        //    {
        //        var product = await _product.GetProduct(id);
        //        if (product == null)
        //            return NotFound("Product Not found");
        //        else
        //            return Ok(product);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //                        "Error retrieving data from the database");
        //    }
        //}

        //[HttpPost]
        //public async Task<ActionResult<IList<Product>>> CreateProduct(Product product)
        //{
        //    try
        //    {
        //        if (product == null)
        //        {
        //            return BadRequest("No product found");
        //        }
        //        product.ProductCategory = null;
        //        _product.CreateProduct(product);
        //        return Ok(await GetProductList());
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //                        "Error retrieving data from the database");
        //    }
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<IList<Product>>> UpdateProduct(Product product, int id)
        //{
        //    try
        //    {
        //        if (product == null)
        //            return BadRequest("Product not found");

        //        var hasProduct = _product.IsExistProduct(id);
        //        if (hasProduct)
        //        {
        //            _product.UpdateProduct(product);
        //            return Ok(await GetProductList());
        //        }
        //        else
        //        {
        //            return BadRequest("Product not found in database");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //                        "Error retrieving data from the database");
        //    }
        //}

        //private async Task<IList<Product>> GetProductList()
        //{
        //    return await _product.GetProducts();
        //}
        //#endregion Product

        //#region ProductCategory

        //[HttpPost]
        //public IActionResult CreateProductCategory(ProductCategory category)
        //{
        //    try
        //    {
        //        var productCategory = _product.CreateCategory(category);
        //        return Ok(productCategory);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //                        "Error retrieving data from the database");
        //    }
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<IList<Product>>> UpdateProductCategory(ProductCategory category, int id)
        //{
        //    try
        //    {
        //        if (category == null)
        //            return BadRequest("Product Category not found");

        //        var hasCategory = _product.IsExistProductCategory(id);
        //        if (hasCategory)
        //        {
        //            _product.UpdateCategory(category);
        //            return Ok(await GetProductCategoryList());
        //        }
        //        else
        //        {
        //            return BadRequest("Product not found in database");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //                        "Error retrieving data from the database");
        //    }
        //}


        //[HttpGet("ProductCategory")]
        //public async Task<ActionResult<IList<ProductCategory>>> GetProductCategories()
        //{
        //    try
        //    {
        //        var productCategories = _product.GetCategories();
        //        if (productCategories == null)
        //            return NotFound("Product Category Not Found");
        //        else
        //            return Ok(productCategories);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //                        "Error retrieving data from the database");
        //    }
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<ProductCategory>> GetProductCategory(int id)
        //{
        //    try
        //    {
        //        var productCategory = await _product.GetCategory(id);
        //        if (productCategory == null)
        //            return NotFound("Product Category Not Found");
        //        else
        //            return Ok(productCategory);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //                        "Error retrieving data from the database");
        //    }
        //}

        //private async Task<IList<ProductCategory>> GetProductCategoryList()
        //{
        //    return await _product.GetCategories();
        //}
        //#endregion ProductCategory

    }
}
