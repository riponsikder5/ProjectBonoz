using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BonozWeb.Pages
{
    public class AllProductBase : ComponentBase
    {

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        public IEnumerable<ProductDTO> Products { get; set; }

        protected override async Task OnInitializedAsync()
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
                            Products = await ProductService.GetProducts();
                            var shoppingCartItems = await ShoppingCartService.GetItems(userIdInt);
                            var totalQty = shoppingCartItems.Sum(i => i.Quantity);

                            ShoppingCartService.RaiseEventOnShoppingCartChanged(totalQty);
                        }
                    }
                }
            }
            else
            {
                NavigationManager.NavigateTo("/login");
            }

           
        }

        protected IOrderedEnumerable<IGrouping<int, ProductDTO>> GetGroupedProductsByCategory()
        {
            return from product in Products
                   group product by product.CategoryId into prodByCatGroup
                   orderby prodByCatGroup.Key
                   select prodByCatGroup;
        }

        protected string GetCategoryName(IGrouping<int, ProductDTO> groupedProductDtos)
        {
            return groupedProductDtos.FirstOrDefault(pg => pg.CategoryId == groupedProductDtos.Key).CategoryName;
        }

        public async Task SearchBarHandler(string filter)
        {
            try
            {
                var allProducts = await ProductService.GetProducts();

                if (string.IsNullOrWhiteSpace(filter))
                {
                    Products = allProducts;
                }
                else
                {
                    Products = allProducts.Where(x => x.Name.ToLower().Contains(filter.ToLower())).ToList();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or display an error message
                Console.WriteLine($"Error in SearchBarHandler: {ex}");
                throw;
            }
        }
    }
}