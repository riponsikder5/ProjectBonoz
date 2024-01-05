namespace BonozApplication.Ifaces
{
    public interface IAddress
    {
        bool CreateAddress(Address address);
        bool UpdateAddress(Address address);
        Task<Address> GetAddress(int id);
    }
}
