using BonozApplication.Managers;
using BonozDomain.AppUser;
using BonozDomain.Models;

namespace BonozAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUser _userManager;

        public UsersController(IUser userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CheckUsers(LoginModel model)
        {
            var user = await _userManager.LoginAsync(model);

            if (user is null)
            {
                return BadRequest("Incorrect credentials");
            }

            return Ok(user);
        }

        //[HttpGet]
        //public async Task<IEnumerable<UserDTO>> GetUsers() =>
        //    await _userManager.GetUsers(UserId);

        //[HttpGet("chats")]
        //public async Task<IEnumerable<UserDTO>> GetUserChats(CancellationToken cancellationToken)
        //{
        //   return await _userManager.GetUserChats(UserId, cancellationToken);
        //}

        [HttpGet("getallusers")]
        public async Task<ActionResult<IList<User>>> GetAllUsers()
        {
            try
            {
                var users = await _userManager.GetAllUsers();

                if (users == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(users);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }
        }

    }
}
