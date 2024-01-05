using BonozDomain.AppUser;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BonozWeb.Authentication
{
    public class BlogAuthenticationStateProvider : AuthenticationStateProvider, IDisposable
    {
        private const string BlogAuthenticationType = "blog-auth";
        private readonly AuthenticationService _authenticationService;

        public BlogAuthenticationStateProvider(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            AuthenticationStateChanged += BlogAuthenticationStateProvider_AuthenticationStateChanged;
        }

        private async void BlogAuthenticationStateProvider_AuthenticationStateChanged(Task<AuthenticationState> task)
        {
            var authState = await task;
            if (authState is not null)
            {
                var userId = Convert.ToInt32(authState.User.FindFirstValue(ClaimTypes.NameIdentifier));
                var displayName = authState.User.FindFirstValue(ClaimTypes.Name);
                var userRole = authState.User.FindFirst(ClaimTypes.Role)?.Value;
                LoggedInUser = new(userId, displayName!, userRole);
            }
        }

        public LoggedInUser LoggedInUser { get; private set; } = new(0, string.Empty, string.Empty);

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var claimsPrincipal = new ClaimsPrincipal();
            var user = await _authenticationService.GetUserFromBrowserStorageAsync();
            if (user is not null)
            {
                claimsPrincipal = GetClaimsPrincipalFromUser(user.Value);
            }
            var authState = new AuthenticationState(claimsPrincipal);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
            return authState;
        }

        public async Task<string?> LoginAsync(LoginModel loginModel)
        {
            var loggedInUser = await _authenticationService.LoginUserAsync(loginModel);
            if (loggedInUser is null)
            {
                return "Invalid credentials";
            }
            var authState = new AuthenticationState(GetClaimsPrincipalFromUser(loggedInUser.Value));
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
            return null;
        }
        
        public async Task<string?> RegisterAsync(RegisterDTO loginModel)
        {
            var loggedInUser = await _authenticationService.RegisterAsync(loginModel);
            if (loggedInUser is null)
            {
                return "Invalid credentials";
            }
            var authState = new AuthenticationState(GetClaimsPrincipalFromUser(loggedInUser.Value));
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
            return null;
        }

        public async Task LogoutAsync()
        {
            await _authenticationService.RemoveUserFromBroserStorageAsync();
            var authState = new AuthenticationState(new ClaimsPrincipal());
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        private static ClaimsPrincipal GetClaimsPrincipalFromUser(LoggedInUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.DisplayName)
            };

            if (!string.IsNullOrEmpty(user.UserRole)) // Check if UserRole is not null or empty
            {
                claims.Add(new Claim(ClaimTypes.Role, user.UserRole));
            }

            var identity = new ClaimsIdentity(claims, BlogAuthenticationType);
            return new ClaimsPrincipal(identity);
        }

        public void Dispose() =>
            AuthenticationStateChanged -= BlogAuthenticationStateProvider_AuthenticationStateChanged;

    }
}
