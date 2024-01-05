namespace BonozDomain.DTO
{
    public record MessageDTO(int ToUserId, int FromUserId, string Message, DateTime SentOn);
}
