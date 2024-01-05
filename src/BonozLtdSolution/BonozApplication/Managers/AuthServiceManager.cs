//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Http;
//using Microsoft.IdentityModel.Tokens;
//using System.Security.Claims;

//namespace BonozApplication.Managers
//{
//    public class AuthServiceManager : IAuthService
//    {

//        //private readonly IHttpContextAccessor _httpContext;
//        //public AuthServiceManager(ISecurity security, IHttpContextAccessor httpContext)
//        //{
//        //    m_UserSecurity = security;
//        //    _httpContext = httpContext;
//        //}

//        private readonly ISecurity m_UserSecurity;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public AuthServiceManager(ISecurity security, IHttpContextAccessor httpContextAccessor)
//        {
//            m_UserSecurity = security;
//            _httpContextAccessor = httpContextAccessor;
//        }



//        public async Task<List<Claim>> SetupAuthClaims(User user, HttpContext context)
//        {
//            var claims = new List<Claim>
//                    {
//                        new Claim(ClaimTypes.IsPersistent, "false", "bool"),
//                        new Claim(ClaimTypes.Name, user.LoginId),
//                        new Claim(ClaimTypes.Name, user.Name),
//                        new Claim("UserId", user.Id.ToString()),
//                    };

//            /// User Restricted Cases
//            var userRestrictedCasesList = m_UserSecurity.GetAllUserRestrictedCases(user.Id);

//            foreach (var caseId in userRestrictedCasesList)
//            {
//                claims.Add(new Claim(caseId.ToString(), caseId.ToString()));
//            }

//            ////Add Primary Access
//            //string PrimaryModule = ((Roles)(user.AppUserRoles.FirstOrDefault().AppRoleId)).GetEnumCategory();
//            //claims.Add(new Claim(PrimaryModule + "_HasAccess", "true", "bool"));

//            //Add Secondary Access
//            foreach (var module in user.AppUserModules)
//            {
//                claims.Add(new Claim(module.ModuleId.ToString() + "_HasAccess", module.HasAccess.ToString(), "bool"));
//            }

//            foreach (var role in user.AppUserRoles)
//            {
//                claims.Add(new Claim(ClaimTypes.Role, role.AppRole.Role));
//                claims.Add(new Claim("RoleId", role.AppRole.Id.ToString()));

//                if (claims.FirstOrDefault(x => x.Type == "Group") == null)
//                {
//                    if (role.AppRole.Role == Roles.Admin.ToString() && role.AppRole.Role == Roles.SuperAdmin.ToString())
//                        claims.Add(new Claim("Group", UserGroupEnum.SA.ToString()));
//                    else if (role.AppRole.Role == Roles.Admin.ToString())
//                        claims.Add(new Claim("Group", UserGroupEnum.AD.ToString()));
//                    else if (role.AppRole.Role == Roles.SuperAdmin.ToString())
//                        claims.Add(new Claim("Group", UserGroupEnum.SA.ToString()));
//                    else if (role.AppRole.Role == Roles.ShopOwner.ToString())
//                        claims.Add(new Claim("Group", UserGroupEnum.SO.ToString()));
//                    else
//                        claims.Add(new Claim("Group", UserGroupEnum.CMR.ToString()));
//                }
//            }

//            foreach (var role in user.AppUserRoles)
//            {
//                claims.Add(new Claim(ClaimTypes.Role, role.AppRole.Role));

//                if (claims.FirstOrDefault(x => x.Type == "Group") == null)
//                {
//                    claims.Add(new Claim("Group", UserGroupEnum.CMR.ToString()));
//                }
//            }

//            try
//            {
//                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
//                context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

//            }
//            catch (Exception ex)
//            {

//                ex.Message.ToString();
//            }
//            return claims;
//        }

//        public async Task LogOff()
//        {
//            await _httpContextAccessor.HttpContext.SignOutAsync();
//            _httpContextAccessor.HttpContext.Session.Clear();
//        }

//        public void AddLoginSession(User user)
//        {
//            var httpContext = _httpContextAccessor.HttpContext;
//            httpContext.Session.SetString("LoginID", user.LoginId);
//            httpContext.Session.SetString("Password", user.Password);
//        }  
//    }
//}
