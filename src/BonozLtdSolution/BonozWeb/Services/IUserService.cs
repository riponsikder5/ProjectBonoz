namespace BonozWeb.Services
{
    public interface IUserService
    {
        Task CreateAddress(AddressDTO address);
        Task UpdateAddress(AddressDTO address);
        Task<AddressDTO> GetAddress(int id);
        Task<IList<User>> GetUsers();
    }
}
