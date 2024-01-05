namespace BonozDomain.Sales
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }

        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        [Required, MaxLength(500)]
        public string Message { get; set; }
        public DateTime SentDateTime { get; set; }

        [ForeignKey(nameof(ChatMessage.SenderId))]
        public User Sender { get; set; }


        [ForeignKey(nameof(ChatMessage.ReceiverId))]
        public User Receiver { get; set; }
    }

}
