using BonozWeb.Helpers;
using System.Net.Http.Headers;

namespace BonozWeb.Components
{
    public class ChatDetailsBase : ComponentBase
    {

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }


        [CascadingParameter(Name = "AuthenticationState")]
        public States.AuthenticationState AuthenticationState { get; set; }

        public int CurrentUserId => AuthenticationState?.User.Id ?? 0;

        [Parameter]
        public UserDTO? SelectedUser { get; set; }

        [Parameter]
        public EventCallback<bool> OnCancel { get; set; }

        [Parameter]
        public EventCallback OnIncomingMessageRecieved { get; set; }

        [Parameter]
        public MessageDTO? NewIncomingMessage { get; set; }
        public IList<MessageDTO> _messages = new List<MessageDTO>();

        public string _newMessage = "";
        public string? _errorMessage;
        public string? _infoMessage;
        public int previousSelectedUserId = 0;
        public bool _scrollToBottom = false;
        public bool _loadingMessages = false;

        protected override async Task OnParametersSetAsync()
        {
            if (NewIncomingMessage is not null)
            {
                _messages.Add(NewIncomingMessage);
                await OnIncomingMessageRecieved.InvokeAsync();
                _scrollToBottom = true;
            }

            if (SelectedUser is not null && SelectedUser.Id != previousSelectedUserId)
            {
                previousSelectedUserId = SelectedUser.Id;
                await LoadMessagesAsync();
                _scrollToBottom = true;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (_scrollToBottom)
            {
                _scrollToBottom = false;
                await JsRuntime.InvokeVoidAsync("window.scrollToLastMessage");
            }
        }

        public async Task LoadMessagesAsync()
        {
            try
            {
                _loadingMessages = true;
                _messages.Clear();
                var allMessages = await GetClient().GetFromJsonAsync<IEnumerable<MessageDTO>>($"https://localhost:44314/api/messages/{SelectedUser.Id}", JsonConverter.JsonSerializerOptions);

                if (allMessages?.Any() == true)
                {
                    _messages = allMessages.ToList();
                    _errorMessage = null;
                    _infoMessage = null;
                }
                else
                {
                    _infoMessage = $"There is no message between you and {SelectedUser.Name}";
                }
            }
            finally
            {
                _loadingMessages = false;
            }

        }

        public async Task HandleCancelClick()
        {
            var isUserHasMessages = _messages.Any();
            var shouldRemoveFromChatsList = !isUserHasMessages;
            await OnCancel.InvokeAsync(shouldRemoveFromChatsList);
        }

        public HttpClient GetClient()
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthenticationState.Token);
            return HttpClient;
        }

        public async Task HandleSendMessageSubmit()
        {
            if (!string.IsNullOrWhiteSpace(_newMessage))
            {
                var sendMessageDto = new MessageSendDto(SelectedUser!.Id, _newMessage);
                var response = await GetClient().PostAsJsonAsync("https://localhost:44314/api/messages", sendMessageDto, JsonConverter.JsonSerializerOptions);
                if (response.IsSuccessStatusCode)
                {
                    var messageDto = new MessageDTO(SelectedUser!.Id, AuthenticationState.User.Id, _newMessage, DateTime.Now);
                    _messages.Add(messageDto);
                    _scrollToBottom = true;
                    _newMessage = "";
                    _errorMessage = null;
                    _infoMessage = null;
                }
                else
                {
                    _errorMessage = "Error sending message";
                }
            }
        }
    }

}
