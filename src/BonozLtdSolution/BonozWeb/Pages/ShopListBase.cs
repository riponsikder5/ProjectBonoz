namespace BonozWeb.Pages
{
    public class ShopListBase : ComponentBase
    {
        [Inject]
        public IShopService ShopService { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        public IEnumerable<Shop> Shops { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Shops = await ShopService.GetShops();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public void EditShop(int ShopId)
        {
            navigationManager.NavigateTo($"Shop/{ShopId}");
        }
    }
}
