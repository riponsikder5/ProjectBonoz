namespace BonozWeb.Components
{
    public class ChatsListBase : ComponentBase
    {

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Parameter]
        public IEnumerable<UserDTO> Chats { get; set; } = Enumerable.Empty<UserDTO>();

        [Parameter]
        public bool Loading { get; set; }

        [Parameter]
        public EventCallback<UserDTO> OnUserSelected { get; set; }

        public async Task HandleUserClick(UserDTO chat)
        {
            await OnUserSelected.InvokeAsync(chat);
        }
    }

}
