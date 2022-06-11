namespace Shared.Events.EmailVerificationSendEmail
{
    public class EmailVerificationSendEmailCompletedEvent : IEvent
    {
        public Guid EmailVerificationId { get; set; }
        public long UserId { get; set; }

    }
}
