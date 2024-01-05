namespace BonozDomain.AppUser
{
    public class ChangePasswordDTO
    {
        public int Id { get; set; }
        public string LoginId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}