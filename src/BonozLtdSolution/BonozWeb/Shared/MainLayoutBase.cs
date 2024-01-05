using BonozDomain.AppUser;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BonozWeb.Shared
{
    public class MainLayoutBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public int UserId { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public string ErrorMessage { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;
                if (user.Identity != null)
                {
                    if (user.Identity.IsAuthenticated)
                    {
                        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                        var userRole = user.FindFirst(ClaimTypes.Role)?.Value;
                        if (!string.IsNullOrEmpty(userIdClaim))
                        {
                            if (int.TryParse(userIdClaim, out int userId))
                            {
                                int userIdInt = int.Parse(userIdClaim);
                                UserId = userIdInt;

                            }
                        }
                    }
                }
                else
                {
                    NavigationManager.NavigateTo("/login");
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
