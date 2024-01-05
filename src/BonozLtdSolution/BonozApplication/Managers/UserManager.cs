using BonozDomain.Models;

namespace BonozApplication.Managers
{
    public class UsersManager : BaseDataManager, IUser
    {
        public UsersManager(BanazDbContext model) : base(model)
        {
        }

        public async Task<IList<User>> GetAllUsers()
        {
            return GetEntityListData<User>();
        }

        public async Task<LoggedInUser?> LoginAsync(LoginModel model)
        {
            var dbUser = await _dbContext.Users
                            .AsNoTracking()
                            .FirstOrDefaultAsync(u => u.Email == model.Username);
            if (dbUser is not null)
            {
                // Login success
                return new LoggedInUser(dbUser.Id, $"{dbUser.Name}".Trim(), $"{dbUser.Role}".Trim());
            }
            else
            {
                // Login failed
                return null;
            }
        }
    }
}