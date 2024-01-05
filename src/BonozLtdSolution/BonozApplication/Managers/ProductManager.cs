using BonozDomain.Sales;

namespace BonozApplication.Managers
{
    public class ProductManager : BaseDataManager, IProduct
    {

        public ProductManager(BanazDbContext context) : base(context)
        {
        }

        #region ProductCategory

        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<ProductCategory> GetCategory(int id)
        {
            return await _dbContext.Categories.SingleOrDefaultAsync(c => c.Id == id);
        }

        public bool CreateCategory(ProductCategory productCategory)
        {
            return AddUpdateEntity(productCategory);
        }

        public bool UpdateCategory(ProductCategory productCategory)
        {
            var category = _dbContext.Categories.FirstOrDefault(c => c.Id == productCategory.Id);

            if (category != null)
            {
                category.Name = productCategory.Name;
                category.IconCSS = productCategory.IconCSS;
                _dbContext.SaveChanges();
                return true;
            }

            else
                return false;
        }

        public async Task DeleteCategory(int id)
        {
            RemoveEntity<ProductCategory>(id);
        }

        #endregion ProductCategory


        #region Product

        public bool CreateProduct(Product product)
        {
           return AddUpdateEntity(product);
        }

        public bool UpdateProduct(Product product)
        {
            var productentity = _dbContext.Products.FirstOrDefault(c => c.Id == product.Id);

            if (productentity != null)
            {
                productentity.Name = product.Name;
                productentity.Price = product.Price;
                productentity.Description = product.Description;
                productentity.ProductCategoryId = product.ProductCategoryId;
                productentity.ShopId= product.ShopId;
                productentity.ImageURL = product.ImageURL;
                productentity.Quantity = product.Quantity;

                _dbContext.SaveChanges();
                return true;
            }

            else
                return false;
        }

        public async Task DeleteProduct(int id)
        {
            RemoveEntity<Product>(id);
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _dbContext.Products.Include(p => p.ProductCategory)
                                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _dbContext.Products.Include(p => p.ProductCategory).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetItemsByProductCategory(int id)
        {
            return await _dbContext.Products.Include(p => p.ProductCategory)
                                     .Where(p => p.ProductCategoryId == id).ToListAsync();
        }

        #endregion


        #region Shop
        public async Task<IList<Shop>> GetShops()
        {
            return GetEntityListData<Shop>();
        }
        #endregion

    }
}