//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;

//namespace BonozApplication.Managers
//{
//    public class SecurityManager : BaseDataManager, ISecurity
//    {
//        public SecurityManager(BanazDbContext model) : base(model)
//        {
//        }

//        public User AuthenticateUserCredentials(string userName, string password)
//        {
//            string pwd = EncryptPassword(password);

//            var userData = _dbContext.Users.Include(x => x.AppUserModules).Include(x => x.AppUserRoles).ThenInclude(appUser => appUser.AppRole)
//                    .FirstOrDefault(x => x.LoginId == userName && x.IsTeamMember == true);

//            if (userData != null)
//            {
//                if (userData.Password == pwd)
//                {
//                    var AppUserActivity = new AppUserActivity
//                    {
//                        LastActiveOn = DateTime.Now,
//                        AppUserId = userData.Id,
//                        LoginId = userData.LoginId
//                    };

//                    AddUpdateEntity(AppUserActivity);
//                }
//                else
//                    return null;

//            }

//            return userData;
//        }

//        public User GetAppUser(int id)
//        {
//            try
//            {
//                return _dbContext.Users.Include(x => x.AppUserRoles).FirstOrDefault(x => x.Id == id);
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(ex.ToString());
//                throw;
//            }
//        }

//        public User GetUserByLoginId(string loginId)
//        {
//            try
//            {
//                return _dbContext.Users.Where(x => x.LoginId.ToUpper() == loginId.ToUpper()).FirstOrDefault();
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(ex.ToString());
//                throw;
//            }

//        }

//        public bool UpdateAppUser(User appUserData, int roleId)
//        {
//            try
//            {
//                _dbContext.Update(appUserData);
//                var role = _dbContext.AppUserRoles.FirstOrDefault(x => x.AppUserId == appUserData.Id);

//                if (role.AppRoleId != roleId)
//                {
//                    _dbContext.AppUserRoles.Remove(role);
//                    _dbContext.AppUserRoles.Add(new AppUserRole { AppRoleId = roleId, AppUserId = appUserData.Id });
//                   // _dbContext.Widgets.RemoveRange(_dbContext.Widgets.Where(c => c.UserId == appUserData.Id));
//                    //ManageUserWidgets(appUserData.Id, roleId, dbModel);
//                }

//                _dbContext.SaveChanges();
//                return true;
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(ex.ToString());
//                throw;
//            }
//        }

//        public int CreateAppUser(User appUserData, int roleId)
//        {
//            try
//            {
//                AppRole role = _dbContext.AppRoles.Find(roleId);

//                AppUserRole appUserRole = new AppUserRole
//                {
//                    AppRole = role,
//                    AppUser = appUserData
//                };

//                string pwd = EncryptPassword(appUserData.Password);
//                appUserData.Password = pwd;

//                appUserData.IsTeamMember = true; // Make this User Active and Ready to GO.
//                appUserData.IsTempPassword = true;

//                appUserData.AppUserRoles.Add(appUserRole);
//                _dbContext.Users.Add(appUserData);

//                _dbContext.SaveChanges(); // This was necessary to make this new User get a Valid Id

//               // ManageUserWidgets(appUserData.Id, roleId, dbModel);

//                _dbContext.SaveChanges();
//                return appUserData.Id;
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(ex.ToString());
//                throw;
//            }
//        }

//        public bool UpdateAppUser(User appUserData)
//        {
//            return AddUpdateEntity(appUserData);
//        }

//        public bool ChangePassword(string loginid, string currentPassword, string newPassword)
//        {
//            try
//            {
//                string pwd = EncryptPassword(currentPassword);

//                User user = _dbContext.Users.Where(x => x.LoginId.ToUpper() == loginid.ToUpper() && x.Password == pwd).FirstOrDefault();

//                if (user == null)
//                    return false;

//                // Compute hash for this new password
//                newPassword = EncryptPassword(newPassword);
//                user.Password = newPassword;
//                user.IsTempPassword = false;
//                user.ModifiedOn = DateTime.Now;

//                _dbContext.SaveChanges();
//                return true;
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(ex.ToString());
//                throw;
//            }
//        }

//        public bool ResetPassword(string email, string loginid, string newPassword)
//        {
//            try
//            {
//                User user = _dbContext.Users.Where(x => x.LoginId.ToUpper() == loginid.ToUpper() && x.Email == email).FirstOrDefault();

//                if (user == null)
//                    return false;

//                // Compute hash for this new password
//                newPassword = EncryptPassword(newPassword);
//                user.Password = newPassword;
//                user.IsTempPassword = false;

//                _dbContext.SaveChanges();

//                return true;
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(ex.ToString());
//                throw;
//            }
//        }

//        public IList<AppRole> GetAllAppRoles()
//        {
//            return GetEntityListData<AppRole>();
//        }

//        public IList<AppUserDTO> GetAppUsers()
//        {
//            try
//            {
//                return (from A in _dbContext.Users
//                        join R in _dbContext.AppUserRoles on A.Id equals R.AppUserId
//                        where R.AppRoleId != 4
//                        select new AppUserDTO
//                        {
//                            Id = A.Id,
//                            LoginId = A.LoginId,
//                            Name = A.Name,
//                            Email = A.Email,
//                            Role = ((Roles)R.AppRoleId).ToString(),
//                            AppUserRoleDesc = R.AppRole.Description,
//                            IsTeamMember = A.IsTeamMember
//                        }).ToList();
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(ex.ToString());
//                throw;
//            }
//        }
//        public bool ResetPassword(string loginid)
//        {
//            try
//            {
//                User user = _dbContext.Users.Where(x => x.LoginId.ToUpper() == loginid.ToUpper()).FirstOrDefault();

//                if (user == null)
//                    return false;

//                // Compute hash for this new password
//                string newPassword = EncryptPassword("TulUser**");

//                user.IsTempPassword = true;
//                user.Password = newPassword;
//                _dbContext.SaveChanges();

//                return true;
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine(ex.ToString());
//                throw;
//            }
//        }

//        public User CheckForExistingAppUserId(string loginId)
//        {
//            return GetEntityFirstRowData<User>(x => x.LoginId.ToUpper() == loginId.ToUpper());
//        }

//        private static string EncryptPassword(string password)
//        {
//            // Create Salt
//            byte[] userBytes = ASCIIEncoding.ASCII.GetBytes("BONOZ");
//            string salt = Convert.ToBase64String(userBytes);

//            // Mix Password & Salt
//            string saltAndPwd = string.Concat(password, salt);

//            UTF8Encoding encoder = new UTF8Encoding();
//            using SHA256 sha256hasher = SHA256.Create();


//            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(saltAndPwd));

//            string hashedPwd = Convert.ToBase64String(hashedDataBytes);
//            return hashedPwd;
//        }


//        public IList<int> GetAllUserRestrictedCases(int userId)
//        {
//            return _dbContext.UserRestrictions.Where(x => x.AppUserId == userId && x.RestrictStatus == true).Select(x => x.CaseId).ToList();
//        }
//    }
//}
