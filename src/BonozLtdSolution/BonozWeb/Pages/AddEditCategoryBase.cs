namespace BonozWeb.Pages
{
    public class AddEditCategoryBase : ComponentBase
    {
        [Parameter]
        public int? Id { get; set; }

        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        public ProductCategoryDTO ProductCategoryDTO { get; set; }

        public string btnText = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            ProductCategoryDTO = new ProductCategoryDTO();

            btnText = Id == null ? "Save" : "Update";
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id != null)
                ProductCategoryDTO = await ProductService.GetCategory((int)Id);
        }

        public async Task HandleSubmit()
        {
            if (Id == null)
            {
                await ProductService.CreateCategory(ProductCategoryDTO);
            }
            else
            {
                await ProductService.UpdateCategory(ProductCategoryDTO);
            }

            navigationManager.NavigateTo("ProductCategory");
        }

        public void CancelProductCategory()
        {
            navigationManager.NavigateTo("ProductCategory");
        }

    }
}