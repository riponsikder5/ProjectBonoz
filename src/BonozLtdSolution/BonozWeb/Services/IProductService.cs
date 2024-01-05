namespace BonozWeb.Services
{
    public interface IProductService
    {
        #region Product

        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProduct(int id);
        Task<IEnumerable<ProductCategoryDTO>> GetProductCategories();
        Task<IEnumerable<ProductDTO>> GetProductByCategory(int categoryId);
        Task<IEnumerable<Shop>> GetShops();
        Task CreateProduct(ProductDTO product);
        Task UpdateProduct(ProductDTO product);
        Task DeleteProduct(int id);

        #endregion

        #region Product Category

        Task CreateCategory(ProductCategoryDTO productCategory);
        Task UpdateCategory(ProductCategoryDTO productCategory);
        Task DeleteCategory(int id);
        Task<ProductCategoryDTO> GetCategory(int id);

        #endregion

    }
}