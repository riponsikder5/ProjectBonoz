namespace BonozWeb.Services
{
    public interface IShopService
    {
        #region Shop

        Task<Shop> GetShop(int id);
        Task<IEnumerable<Shop>> GetShops();
        Task CreateShop(Shop shop);
        Task UpdateShop(Shop shop);
        //Task DeleteShop(int id);

        #endregion
    }
}