namespace BonozDomain.AppUser
{
    public class AppRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public virtual ICollection<UserRoles> AppUserRoles { get; set; }
    }
}