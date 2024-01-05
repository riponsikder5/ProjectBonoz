using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BonozWeb.Pages
{
    public class ProductDetailsBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        public ProductDTO Product { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Product = await ProductService.GetProduct(Id);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected async Task AddToCart_Click(CartItemToAddDTO cartItemToAddDto)
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
                        if (!string.IsNullOrEmpty(userIdClaim))
                        {
                            if (int.TryParse(userIdClaim, out int userId))
                            {
                                int userIdInt = int.Parse(userIdClaim);
                                var cartItemDto = await ShoppingCartService.AddItem(cartItemToAddDto, userIdInt);
                                NavigationManager.NavigateTo("/ShoppingCart");
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
                //Log Exception
                throw;
            }
        }
    }
}
