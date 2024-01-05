namespace BonozDomain.DTO
{
    public class LoginDTO
    {
        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required, MaxLength(20), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
