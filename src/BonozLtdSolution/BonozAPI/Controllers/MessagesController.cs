using BonozAPI.Hubs;
using BonozApplication.ChatHub;
using Microsoft.AspNetCore.SignalR;

namespace BonozAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : BaseController
    {
        private readonly IChat _chatManager;
        private readonly IHubContext<BonozChatHub, IBonozChatHubClient> _hubContext;

        public MessagesController(IChat chatManager, IHubContext<BonozChatHub, IBonozChatHubClient> hubContext)
        {
            _chatManager = chatManager;
            _hubContext = hubContext;
        }

        // /api/messages
        [HttpPost("")]
        public async Task<IActionResult> SendMessage(MessageSendDto messageDto, CancellationToken cancellationToken)
        {
            if (messageDto.ToUserId <= 0 || string.IsNullOrWhiteSpace(messageDto.Message))
                return BadRequest();

            var message = new ChatMessage
            {
                SenderId = base.UserId,
                ReceiverId = messageDto.ToUserId,
                Message = messageDto.Message,
                SentDateTime = DateTime.Now
            };

            var result = _chatManager.SendMessage(message, cancellationToken);

            if (result)
            {
                var responseMessageDto = new MessageDTO(message.ReceiverId, message.SenderId, message.Message, message.SentDateTime);
                await _hubContext.Clients.User(messageDto.ToUserId.ToString())
                            .MessageRecieved(responseMessageDto);
                return Ok();
            }
            else
            {
                return StatusCode(500, "Unable to send message");
            }
        }

        [HttpGet("{otherUserId:int}")]
        public async Task<IEnumerable<MessageDTO>> GetMessages(int otherUserId, CancellationToken cancellationToken)
        {
            return await _chatManager.GetMessages(otherUserId, UserId, cancellationToken);
        }
    }
}