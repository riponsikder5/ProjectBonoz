using BonozDomain.Models;

namespace BonozApplication.Ifaces
{
    public interface IAccount
    {
        Task<User> CheckLogin(string username, string password, CancellationToken cancellationToken);
        Task<bool> IsUserNameExists(string username, CancellationToken cancellationToken);
        Task<LoggedInUser?> Register(User user );
        Task<LoggedInUser?> LoginAsync(LoginModel model);

    }
}
