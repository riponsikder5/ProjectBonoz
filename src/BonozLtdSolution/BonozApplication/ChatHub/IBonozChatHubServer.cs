using BonozDomain.DTO;

namespace BonozApplication.ChatHub
{
    public interface IBonozChatHubServer
    {
        Task SetUserOnline(UserDTO user);
    }
}
