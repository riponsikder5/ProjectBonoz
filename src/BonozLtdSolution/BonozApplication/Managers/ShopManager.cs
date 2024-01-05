using BonozDomain.Sales;

namespace BonozApplication.Managers
{
    public class ShopManager : BaseDataManager, IShop
    {

        public ShopManager(BanazDbContext context) : base(context)
        {
        }

        #region Shop

        public bool CreateShop(Shop Shop)
        {
           return AddUpdateEntity(Shop);
        }

        public bool UpdateShop(Shop Shop)
        {
            var Shopentity = _dbContext.Shops.FirstOrDefault(c => c.Id == Shop.Id);

            if (Shopentity != null)
            {
                Shopentity.Name = Shop.Name;
                Shopentity.Description = Shop.Description;
                Shopentity.Status = Shop.Status;

                _dbContext.SaveChanges();
                return true;
            }

            else
                return false;
        }

        public async Task DeleteShop(int id)
        {
            RemoveEntity<Shop>(id);
        }

        public async Task<Shop> GetShop(int id)
        {
            return await _dbContext.Shops.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Shop>> GetShops()
        {
            return await _dbContext.Shops.ToListAsync();
        }

        #endregion
    }
}