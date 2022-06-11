namespace Shared.Entities.Common
{
    public class UserTokenEntity
    {
        public string Token { get; set; }

        public DateTime? ExpiresDate { get; set; }
    }
}
