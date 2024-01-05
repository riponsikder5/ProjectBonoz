namespace BonozAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddress _address;

        public AddressController(IAddress address)
        {
            _address = address;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AddressDTO>> GetAddressById(int id)
        {
            try
            {
                var address = await _address.GetAddress(id);

                if (address == null)
                {
                    return NotFound();
                }
                else
                {
                    var addressDto = address.ConvertToDto();

                    return Ok(addressDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAddress(AddressDTO addressDTO)
        {
            try
            {
                if (addressDTO != null)
                {
                    var addressEntity = addressDTO.ConvertToEntity();
                    _address.CreateAddress(addressEntity);

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
        public async Task<ActionResult> UpdateAddress(AddressDTO addressDTO, int id)
        {
            try
            {
                if (addressDTO != null && IsExistCategory(id))
                {
                    var categoryEntity = addressDTO.ConvertToEntity();
                    _address.UpdateAddress(categoryEntity);

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

        private bool IsExistCategory(int id)
        {
            var data = _address.GetAddress(id);
            if (data == null)
                return false;
            else
                return true;
        }

    }
}
