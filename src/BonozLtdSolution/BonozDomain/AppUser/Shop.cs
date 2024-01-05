namespace BonozDomain.AppUser
{
    public class Shop
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ShopStatus Status { get; set; }
    }

    public enum ShopStatus
    {
        [Display(Name = "Pending")]
        Pending = 0,
        [Display(Name = "Approve")]
        Approve = 1,
        [Display(Name = "Reject")]
        Reject = 2
    }
}