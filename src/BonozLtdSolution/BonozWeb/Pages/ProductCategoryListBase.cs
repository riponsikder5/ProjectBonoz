namespace BonozWeb.Pages
{
    public class ProductCategoryListBase : ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        public IEnumerable<ProductCategoryDTO> ProductCategory { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ProductCategory = await ProductService.GetProductCategories();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public void CreateCategory()
        {
            navigationManager.NavigateTo("AddEditCategory");
        }

        public void EditCategory(int categoryId)
        {
            navigationManager.NavigateTo($"AddEditCategory/{categoryId}");
        }

        public void DeleteCategory(int categoryId)
        {
            ProductService.DeleteCategory(categoryId);
        }
    }
}