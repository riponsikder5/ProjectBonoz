namespace BonozDomain.AppUser
{
    public class UserRoles
    {
        public int AppUserId { get; set; }
        public User AppUser { get; set; }

        public int AppRoleId { get; set; }
        public AppRole AppRole { get; set; }
    }
}