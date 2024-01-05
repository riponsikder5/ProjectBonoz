namespace BonozWeb.Pages
{
    public class RoleListBase : ComponentBase
    {
        [Inject]
        public IUserService Service { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        public IEnumerable<User> Users { get; set; }
        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Users = await Service.GetUsers();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public void EditRole(int RoleId)
        {
            navigationManager.NavigateTo($"Role/{RoleId}");
        }
    }
}
