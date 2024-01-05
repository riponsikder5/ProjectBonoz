using BonozDomain.DTO;
using BonozDomain.Models;

namespace BonozApplication.Ifaces
{
    public interface IUser
    {
        //Task<IEnumerable<UserDTO>> GetUsers(int currentUserId);
        //Task<IEnumerable<UserDTO>> GetUserChats(int currentUserId, CancellationToken cancellationToken);

        Task<LoggedInUser?> LoginAsync(LoginModel model);
        Task<IList<User>> GetAllUsers(); 
    }
}
