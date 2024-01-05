namespace BonozApplication.Ifaces
{
    public interface IShop
    {
        #region Shop

        bool CreateShop(Shop shop);
        bool UpdateShop(Shop Shop);
        Task<IEnumerable<Shop>> GetShops();
        Task<Shop> GetShop(int id);

        #endregion Shop
    }
}