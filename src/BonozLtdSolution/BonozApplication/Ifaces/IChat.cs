using BonozDomain.DTO;

namespace BonozApplication.Ifaces
{
    public interface IChat
    {
        Task<IEnumerable<MessageDTO>> GetMessages(int otherUserId, int currentUserId, CancellationToken cancellationToken);
        bool SendMessage(ChatMessage message, CancellationToken cancellationToken);

    }
}
