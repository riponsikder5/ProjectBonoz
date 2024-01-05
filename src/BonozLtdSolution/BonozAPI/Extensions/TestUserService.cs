using BonozDomain.Models;

namespace BonozAPI.Extensions
{
    public class TestUserService
    {
        private readonly IUser _user;

        public TestUserService(IUser user)
        {
            _user = user;
        }
        public async Task<LoggedInUser?> LoginAsync(LoginModel model)
        {
            return await _user.LoginAsync(model);
        }
    }
}
