namespace BonozDomain.AppUser
{
    public abstract class AppUserModuleBase
    {
        public int Id { get; set; }

        public Module ModuleId { get; set; }
        public bool HasAccess { get; set; }
    }

    public class AppUserModule : AppUserModuleBase
    {
        public int AppUserId { get; set; }
        public User AppUser { get; set; }

        public int AppRoleId { get; set; }
        public AppRole AppRole { get; set; }
    }
}