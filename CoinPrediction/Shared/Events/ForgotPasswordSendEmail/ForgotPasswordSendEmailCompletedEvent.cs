namespace Shared.Events.ForgotPasswordSendEmail
{
    public class ForgotPasswordSendEmailCompletedEvent : IEvent
    {
        public Guid ForgotPasswordId { get; set; }

        public DateTime ExpiryDate { get; set; }
        
        public int Code { get; set; }

        public long UserId { get; set; }

    }
}
