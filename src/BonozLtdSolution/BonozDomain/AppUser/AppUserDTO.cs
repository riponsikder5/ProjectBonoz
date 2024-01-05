namespace BonozDomain.AppUser
{
    public class AppUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AppUserRoleDesc { get; set; }

        public string Email { get; set; }
        public DateTime LastActiveDate { get; set; }

        public string Role { get; set; }

        public bool IsTeamMember { get; set; }

        public string LoginId { get; set; }

        public string IsTeamMemberFormatted
        {
            get
            {
                if (IsTeamMember == true)
                    return "Active";
                else return "Not Active";
            }
        }
    }
}