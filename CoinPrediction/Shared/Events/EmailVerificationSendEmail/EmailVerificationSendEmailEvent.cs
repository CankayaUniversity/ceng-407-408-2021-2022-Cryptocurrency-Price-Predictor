namespace Shared.Events.EmailVerificationSendEmail
{
    public class EmailVerificationSendEmailEvent:IEvent
    {
        public string Email { get; set; }
        public long UserId { get; set; }

    }
}
