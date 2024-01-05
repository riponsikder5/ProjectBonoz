namespace BonozDomain.AppUser
{
    public class AppUserActivity
    {
        public int Id { get; set; }
        public string LoginId { get; set; }
        public DateTime LastActiveOn { get; set; }
        public int AppUserId { get; set; }
    }
}