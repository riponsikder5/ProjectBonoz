using BonozApplication.ChatHub;
using Microsoft.AspNetCore.SignalR;

namespace BonozAPI.Hubs
{
    public class BonozChatHub : Hub<IBonozChatHubClient>, IBonozChatHubServer
    {
        private static readonly IDictionary<int, UserDTO> _onlineUsers = new Dictionary<int, UserDTO>();

        public BonozChatHub()
        {

        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task SetUserOnline(UserDTO user)
        {
            await Clients.Caller.OnlineUsersList(_onlineUsers.Values);
            if (!_onlineUsers.ContainsKey(user.Id))
            {
                _onlineUsers.Add(user.Id, user);
                await Clients.Others.UserIsOnline(user.Id);
            }
        }
    }
}

