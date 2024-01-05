namespace BonozWeb.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public AccountService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public Task Register(RegisterDTO orderDTO)
        {
            throw new NotImplementedException();
        }
    }
}
