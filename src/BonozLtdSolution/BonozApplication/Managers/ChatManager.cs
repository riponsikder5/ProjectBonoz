using BonozDomain.DTO;

namespace BonozApplication.Managers
{
    public class ChatManager : BaseDataManager, IChat
    {
        public ChatManager(BanazDbContext context) : base(context)
        {
        }

        public bool SendMessage(ChatMessage message, CancellationToken cancellationToken)
        {
            try
            {
                if (message != null)
                {
                    _dbContext.ChatMessages.AddAsync(message, cancellationToken);
                    _dbContext.SaveChangesAsync(cancellationToken);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<IEnumerable<MessageDTO>> GetMessages(int otherUserId, int currentUserId, CancellationToken cancellationToken)
        {
            try
            {
                var messages = await _dbContext.ChatMessages
                                .AsNoTracking()
                                .Where(m =>
                                    (m.SenderId == otherUserId && m.ReceiverId == currentUserId)
                                || (m.ReceiverId == otherUserId && m.SenderId == currentUserId))
                                .Select(m => new MessageDTO(m.ReceiverId, m.SenderId, m.Message, m.SentDateTime))
                                .ToListAsync(cancellationToken);

                return messages;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
