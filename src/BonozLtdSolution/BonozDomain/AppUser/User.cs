using System.ComponentModel;

namespace BonozDomain.AppUser
{
    public class User
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Login Id is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be a minimum length of 6 and a maximum length of 20")]
        public string Password { get; set; }

        [Display(Name = "Name")]
        [MaxLength(100), Required(ErrorMessage = "Name Id is Required")]
        public string Name { get; set; }

        [MaxLength(20)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not a valid")]
        public string Email { get; set; }

        public DateTime AddedOn { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(17)]
        public string Phone { get; set; }
        public Roles Role { get; set; }
        public ICollection<UserRoles> UserRoles { get; set; }

    }

    #region enum
    #region Roles
    public enum Roles
    {
        None = 0,

        [Category("SA")]
        SuperAdmin = 1,  // Banaz

        [Category("AD")]
        Admin = 2,

        [Category("SO")]
        ShopOwner = 3,

        [Category("CMR")]
        Customer = 4,
    }
    #endregion

    #region UserGroupEnum
    public enum UserGroupEnum
    {
        None = 0,
        SA = 1,
        AD = 2,
        SO = 3,
        CMR = 4,
    }

    #endregion

    #region Module
    public enum Module
    {
        [Display(Name = "General"), Description("General"), Category("None")]
        None = 0,

        [Display(Name = "Super Admin"), Description("Super Admin"), Category("SA")]
        SA = 1,

        [Display(Name = "Admin"), Description("Admin"), Category("AD")]
        AD = 2,

        [Display(Name = "Shop Owner"), Description("Shop Owner"), Category("SO")]
        SO = 3,

        [Display(Name = "Customer"), Description("Customer"), Category("CMR")]
        CMR = 3,
    }

    #endregion Module
    #endregion enum
}
