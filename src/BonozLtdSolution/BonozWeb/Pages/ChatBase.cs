using BonozApplication.ChatHub;
using BonozWeb.Helpers;
using Microsoft.AspNetCore.SignalR.Client;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace BonozWeb.Pages
{
    public class ChatBase : ComponentBase
    {
        [CascadingParameter(Name = "AuthenticationState")]
        public States.AuthenticationState AuthenticationState { get; set; }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }


        public HubConnection? _hubConnection;

        public bool _loadingUsers = false;
        public ICollection<UserDTO> Users { get; set; } = new HashSet<UserDTO>();
        public IList<UserDTO> Chats { get; set; } = new List<UserDTO>();

        public UserDTO? _selectedUser = null;
        public MessageDTO? _newIncomingMessage = null;

        public async Task<bool> IsTokenExpiredAsync()
        {
            var jwt = new JwtSecurityToken(AuthenticationState.Token);
            if (jwt.ValidTo <= DateTime.Now)
            {
                // Token has expired
                // Navigate to login page
                await HandleLogout();
                return true;
            }
            return false;
        }

        protected override async void OnInitialized()
        {
            base.OnInitialized();
            if (!AuthenticationState.IsAuthenticated)
            {
                navigationManager.NavigateTo("/login");
            }
            else
            {
                if (!await IsTokenExpiredAsync())
                {
                    _hubConnection = ConfigureHubConnection();

                    _loadingUsers = true;
                    var userListTask = GetClient().GetFromJsonAsync<ICollection<UserDTO>>("https://localhost:44314/api/users", JsonConverter.JsonSerializerOptions);
                    var chatListTask = GetClient().GetFromJsonAsync<IList<UserDTO>>("https://localhost:44314/api/users/chats", JsonConverter.JsonSerializerOptions);

                    await _hubConnection.StartAsync();

                    var usersList = await userListTask;
                    if (usersList is not null)
                    {
                        Users = usersList;
                    }

                    var chatsList = await chatListTask;
                    if (chatsList is not null)
                    {
                        Chats = chatsList;
                    }
                    _loadingUsers = false;
                    await _hubConnection.SendAsync(nameof(IBonozChatHubServer.SetUserOnline), AuthenticationState.User);
                    StateHasChanged();
                }
            }
        }

        public HttpClient GetClient()
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthenticationState.Token);
            return HttpClient;
        }

        public async Task OnUserSelected(UserDTO user)
        {
            _selectedUser = user;

            var selectedChatUser = Chats.FirstOrDefault(c => c.IsSelected);
            if (selectedChatUser is not null)
            {
                selectedChatUser.IsSelected = false;
            }

            var chatUser = Chats.FirstOrDefault(c => c.Id == user.Id);
            if (chatUser is null)
            {
                user.IsSelected = true;
                Chats.Add(user);
            }
            else
            {
                chatUser.IsSelected = true;
            }

        }

        public async Task HandleChatDetailsCancel(bool shouldRemoveFromChatsList)
        {
            if (shouldRemoveFromChatsList)
            {
                Chats.Remove(_selectedUser);
            }
            _selectedUser = null;
        }

        public async Task HandleLogout()
        {
            await JsRuntime.InvokeVoidAsync("window.removeFromStorage", States.AuthenticationState.AuthStoreKey);
            AuthenticationState.UnLoadState();
            //NavigationManager.NavigateTo("/");
        }

        public HubConnection ConfigureHubConnection()
        {
            var hubConnection = new HubConnectionBuilder()
                                .WithUrl(navigationManager.ToAbsoluteUri("/hubs/blazing-chat"),
                                   options => options.AccessTokenProvider = () => Task.FromResult(AuthenticationState.Token))
                                .Build();

            hubConnection.On<UserDTO>(nameof(IBonozChatHubClient.UserConnected), (newUser) =>
            {
                Users.Add(newUser);
                StateHasChanged();
            });

            hubConnection.On<ICollection<UserDTO>>(nameof(IBonozChatHubClient.OnlineUsersList), (onlineUsers) =>
            {
                foreach (var user in Users)
                {
                    if (onlineUsers.Any(u => u.Id == user.Id))
                    {
                        user.IsOnline = true;
                    }
                }
                StateHasChanged();
            });

            hubConnection.On<int>(nameof(IBonozChatHubClient.UserIsOnline), (userId) =>
            {
                var user = Users.FirstOrDefault(u => u.Id == userId);
                if (user is not null)
                {
                    user.IsOnline = true;
                    StateHasChanged();
                }
            });

            hubConnection.On<MessageDTO>(nameof(IBonozChatHubClient.MessageRecieved), (messageDto) =>
            {
                var fromUser = Users.FirstOrDefault(u => u.Id == messageDto.FromUserId);

                if (!Chats.Any(c => c.Id == messageDto.FromUserId))
                {
                    Chats.Insert(0, fromUser!);
                }
                else
                {
                    if (_selectedUser?.Id == messageDto.FromUserId)
                    {
                        // Append the message to the messages list
                        _newIncomingMessage = messageDto;
                    }
                }
                StateHasChanged();
            });

            return hubConnection;
        }

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection is not null)
                await _hubConnection.DisposeAsync();
        }
    }

}
