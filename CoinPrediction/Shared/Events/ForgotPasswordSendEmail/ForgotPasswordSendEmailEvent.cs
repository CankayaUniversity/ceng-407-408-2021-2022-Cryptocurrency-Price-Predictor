namespace Shared.Events.ForgotPasswordSendEmail
{
    public class ForgotPasswordSendEmailEvent : IEvent
    {
        public string Email { get; set; }
        public long UserId { get; set; }

    }
}
