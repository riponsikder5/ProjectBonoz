using BonozDomain.AppUser;
using Microsoft.AspNetCore.Components.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace BonozWeb.Pages
{
    public class AddEditShopBase : ComponentBase
    {
        [Parameter]
        public int? Id { get; set; }
        public int UserId { get; set; }
        [Inject]
        public IShopService ShopService { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Shop Shop { get; set; }

        public string btnText = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            Shop = new Shop();
            
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

            btnText = Id == null ? "Save" : "Update";
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id != null)
                Shop = await ShopService.GetShop((int)Id);
        }

        public async Task HandleSubmit()
        {
            if (Id == null)
            {
                Shop.UserId = UserId;
                await ShopService.CreateShop(Shop);
            }
            else
            {
                await ShopService.UpdateShop(Shop);

            }

        }

        public void CancelShop()
        {
            NavigationManager.NavigateTo("Shop");
        }

        public string GetShopStatusDisplayName(ShopStatus status)
        {
            var fieldInfo = status.GetType().GetField(status.ToString());
            var displayAttribute = fieldInfo.GetCustomAttributes(false).OfType<DisplayAttribute>().FirstOrDefault();
            return displayAttribute?.Name ?? status.ToString();
        }
    }
}