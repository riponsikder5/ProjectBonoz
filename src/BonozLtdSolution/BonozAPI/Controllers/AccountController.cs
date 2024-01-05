using BonozDomain.AppUser;
using BonozDomain.Models;
using Microsoft.AspNetCore.SignalR;
using System.Security.Cryptography;
using System.Text;


namespace BonozAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly IAccount _accountManager;

        public AccountController(TokenService tokenService, IAccount accountManager)
        {
            _tokenService = tokenService;
            _accountManager = accountManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto, CancellationToken cancellationToken)
        {
            var usernameExists = await _accountManager.IsUserNameExists(dto.Username, cancellationToken);

            if (usernameExists)
            {
                return BadRequest($"[{nameof(dto.Username)}] already exists");
            }

            string pwd = EncryptPassword(dto.Password);

            var user = new User
            {
                UserName = dto.Username,
                AddedOn = DateTime.Now,
                Name = dto.Name,
                Password = pwd,
            };

           var registerUser = await _accountManager.Register(user);

            //await _hubContext.Clients.All.UserConnected(new UserDto(user.Id, user.Name));
            return Ok(registerUser);
        }

        [HttpPost]
        public async Task<IActionResult> CheckUsers(LoginModel model)
        {
            model.Password = EncryptPassword(model.Password);
            var user = await _accountManager.LoginAsync(model);

            if (user is null)
            {
                return BadRequest("Incorrect credentials");
            }

            return Ok(user);
        }


        //[HttpPost("login")]
        //public async Task<IActionResult> Login(LoginDTO dto, CancellationToken cancellationToken)
        //{
        //    string pwd = EncryptPassword(dto.Password);

        //    var user = await _accountManager.CheckLogin(dto.Username, pwd, cancellationToken);
        //    if (user is null)
        //    {
        //        return BadRequest("Incorrect credentials");
        //    }

        //    return Ok(GenerateToken(user));
        //}


        private AuthResponseDTO GenerateToken(User user)
        {
            var token = _tokenService.GenerateJWT(user);
            return new AuthResponseDTO(new UserDTO(user.Id, user.Name), token);
        }

        private static string EncryptPassword(string password)
        {
            byte[] userBytes = ASCIIEncoding.ASCII.GetBytes("BONOZ");
            string salt = Convert.ToBase64String(userBytes);

            // Mix Password & Salt
            string saltAndPwd = string.Concat(password, salt);

            UTF8Encoding encoder = new UTF8Encoding();
            using SHA256 sha256hasher = SHA256.Create();


            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(saltAndPwd));

            string hashedPwd = Convert.ToBase64String(hashedDataBytes);
            return hashedPwd;
        }
    }
}
