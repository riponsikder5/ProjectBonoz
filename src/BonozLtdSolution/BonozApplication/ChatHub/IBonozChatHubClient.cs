using BonozDomain.DTO;

namespace BonozApplication.ChatHub
{
    public interface IBonozChatHubClient
    {
        Task UserConnected(UserDTO user);
        Task OnlineUsersList(IEnumerable<UserDTO> users);
        Task UserIsOnline(int userId);

        Task MessageRecieved(MessageDTO messageDto);
    }
}
