namespace BonozDomain.Models
{
    public record struct LoggedInUser(int UserId, string DisplayName, string UserRole)
    {
        public readonly bool IsEmpty => UserId == 0;
    }
}   
