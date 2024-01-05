using BonozDomain.Models;

namespace BonozApplication.Managers
{
    public class AccountManager : BaseDataManager, IAccount
    {
        public AccountManager(BanazDbContext model) : base(model)
        {
        }

        public async Task<LoggedInUser?> LoginAsync(LoginModel model)
        {
            var dbUser = await _dbContext.Users
                            .AsNoTracking()
                            .FirstOrDefaultAsync(u => u.UserName == model.Username 
                                                && u.Password == model.Password );
            if (dbUser is not null)
            {
                // Login success
                return new LoggedInUser(dbUser.Id, $"{dbUser.Name}".Trim(), $"{dbUser.Role.ToString()}".Trim());
            }
            else
            {
                // Login failed
                return null;
            }
        }

        public async Task<LoggedInUser?> Register(User user)
        {
            try
            {
               await _dbContext.Users.AddAsync(user);
               await _dbContext.SaveChangesAsync();
               return new LoggedInUser(user.Id, $"{user.Name}".Trim(), $"{user.Role}".Trim());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<User> CheckLogin(string username, string password, CancellationToken cancellationToken)
        {
            try
            {
                return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password, cancellationToken);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<bool> IsUserNameExists(string username, CancellationToken cancellationToken)
        {
            try
            {
                return await _dbContext.Users
                       .AsNoTracking()
                       .AnyAsync(u => u.UserName == username, cancellationToken);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}