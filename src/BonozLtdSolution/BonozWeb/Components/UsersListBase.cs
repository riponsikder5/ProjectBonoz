using BonozDomain.DTO;

namespace BonozWeb.Components
{
    public class UsersListBase : ComponentBase
    {

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Parameter]
        public EventCallback<UserDTO> OnUserSelected { get; set; }

        [Parameter]
        public bool Loading { get; set; }

        [Parameter]
        public ICollection<UserDTO> Users { get; set; } = default!;

        public async Task HandleUserClick(UserDTO user)
        {
            await OnUserSelected.InvokeAsync(user);
        }
    }
}
