namespace BonozWeb.Pages
{
    public class ProductListBase : ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        public IEnumerable<ProductDTO> Products { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Products = await ProductService.GetProducts();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public void CreateProduct()
        {
            navigationManager.NavigateTo("AddEditProduct");
        }

        public void EditProduct(int productId)
        {
            navigationManager.NavigateTo($"AddEditProduct/{productId}");
        }

        public void DeleteProduct(int productId)
        {
            ProductService.DeleteProduct(productId);
        }
    }
}
