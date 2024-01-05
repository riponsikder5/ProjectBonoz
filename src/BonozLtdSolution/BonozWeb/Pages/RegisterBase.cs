namespace BonozWeb.Pages
{
    public class RegisterBase : ComponentBase
    {
        [Inject]
        public BlogAuthenticationStateProvider BlogAuthStateProvider { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected RegisterDTO _model { get; set; } = new();
        protected bool _isBusy = false;
        protected string errorMessage = null;
        public bool _isProcessing = false;


        protected async Task RegisterAsync()
        {
            errorMessage = null;
            _isProcessing = true;
            try
            {
                errorMessage = await BlogAuthStateProvider.RegisterAsync(_model);
                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    _isProcessing = false;
                }
                else
                {
                    var loggedInUser = BlogAuthStateProvider.LoggedInUser;
                    if (loggedInUser.IsEmpty)
                    {
                        errorMessage = "Could not log in. Please try again";
                        _isProcessing = false;
                    }
                    else
                    {
                        // Successful login
                        NavigationManager.NavigateTo("/");
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                _isBusy = false;
            }
        }
    }
}
