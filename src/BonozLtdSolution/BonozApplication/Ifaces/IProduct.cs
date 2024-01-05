namespace BonozApplication.Ifaces
{
    public interface IProduct
    {
        #region Product

        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task<IEnumerable<Product>> GetItemsByProductCategory(int id);
        Task DeleteProduct(int id);

        #endregion Product

        #region ProductCategory

        bool CreateCategory(ProductCategory productCategory);
        bool UpdateCategory(ProductCategory productCategory);
        Task<IEnumerable<ProductCategory>> GetCategories();
        Task<ProductCategory> GetCategory(int id);
        Task DeleteCategory(int id);

        #endregion ProductCategory

        #region Shops
        Task<IList<Shop>> GetShops();

        #endregion
    }
}