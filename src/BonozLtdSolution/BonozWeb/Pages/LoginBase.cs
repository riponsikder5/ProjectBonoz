namespace BonozWeb.Pages
{
    public class LoginBase : ComponentBase
    {

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public BlogAuthenticationStateProvider BlogAuthStateProvider { get; set; }

        public LoginModel _model = new();

        public bool _isProcessing = false;
        public string? _error = null;

        public async Task LoginAsync()
        {
            _error = null;
            _isProcessing = true;
            try
            {
                _error = await BlogAuthStateProvider.LoginAsync(_model);
                if (!string.IsNullOrWhiteSpace(_error))
                {
                    _isProcessing = false;
                }
                else
                {
                    var loggedInUser = BlogAuthStateProvider.LoggedInUser;
                    if (loggedInUser.IsEmpty)
                    {
                        _error = "Could not log in. Please try again";
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
                _isProcessing = false;
                _error = ex.Message;
            }
        }
    }

}
