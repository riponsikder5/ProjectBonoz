using BonozDomain.Sales;
using Microsoft.EntityFrameworkCore;

namespace BonozApplication.Managers
{
    public class AddressManager : BaseDataManager, IAddress
    {
        public AddressManager(BanazDbContext model) : base(model)
        {
        }

        public bool UpdateAddress(Address address)
        {
            var getAddress = _dbContext.Addresses.FirstOrDefault(c => c.UserId == address.UserId);

            if (getAddress != null)
            {
                getAddress.HouseNumber = address.HouseNumber;
                getAddress.RoadNumber = address.RoadNumber;
                getAddress.District = address.District;
                getAddress.Division = address.Division;
                getAddress.PoliceStation = address.PoliceStation;
                getAddress.Village = address.Village;
                _dbContext.SaveChanges();
                return true;
            }

            else
                return false;
        }

        public bool CreateAddress(Address address)
        {
            return AddUpdateEntity(address);
        }

        public async Task<Address> GetAddress(int id)
        {
            return await _dbContext.Addresses.SingleOrDefaultAsync(c => c.UserId == id);
        }
    }
}