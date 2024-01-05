using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BonozWeb.Pages
{
    public class OrdersBase : ComponentBase
    {

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IOrderService OrderService { get; set; }

        public bool Popup { get; set; }

        public IList<OrderDTO> Orders = new List<OrderDTO>();
        public IList<OrderDetailsDTO> OrderDetailsDTO = new List<OrderDetailsDTO>();

        protected override async Task OnInitializedAsync()
        {
            try
            {

                Popup = false;

                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;

                if (user.Identity != null)
                {
                    if (user.Identity.IsAuthenticated)
                    {
                        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                        if (!string.IsNullOrEmpty(userIdClaim))
                        {
                            if (int.TryParse(userIdClaim, out int userId))
                            {
                                int userIdInt = int.Parse(userIdClaim);
                                Orders = await OrderService.GetOrders(userIdInt);
                            }
                        }
                    }
                }
                else
                {
                    NavigationManager.NavigateTo("/login");
                }

            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }


        public async Task ShowDetails(int id)
        {
            OrderDetailsDTO = await OrderService.GetOrdersDetails(id);
            Popup = true;
        }

        public void ShowPopup()
        {
            Popup = true;
        }
        public void HidePopup()
        {
            Popup = false;
        }
    }
}
