namespace BonozDomain.DTO
{
    public class RegisterDTO
    {
        [Required, MaxLength(25)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required, MaxLength(20), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
