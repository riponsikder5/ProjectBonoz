using BonozDomain.AppUser;
using BonozDomain.DTO.SalesDTO;

namespace BonozAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShop _Shop;
        private readonly IOrder _order;
        public ShopController(IShop Shop, IOrder order)
        {
            _Shop = Shop;
            _order = order;
        }

        [HttpPost]
        public async Task<ActionResult> CreateShop(Shop shop)
        {
            try
            {
                if (shop == null)
                {
                    return BadRequest("No Shop found");
                }
                _Shop.CreateShop(shop);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateShop(Shop shop, int id)
        {
            try
            {
                if (shop == null)
                    return BadRequest("Shop not found");

                var hasShop = IsExistShop(id);

                if (hasShop != false)
                {
                    _Shop.UpdateShop(shop);
                    return Ok();
                }
                else
                {
                    return BadRequest("Shop not found in database");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shop>>> GetItems()
        {
            try
            {
                var shops = await _Shop.GetShops();

                if (shops == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(shops);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Shop>> GetItem(int id)
        {
            try
            {
                var shop = await _Shop.GetShop(id);

                if (shop == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(shop);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        private bool IsExistShop(int id)
        {
            var data = _Shop.GetShop(id);
            if (data == null)
                return false;
            else
                return true;
        }
    }
}
