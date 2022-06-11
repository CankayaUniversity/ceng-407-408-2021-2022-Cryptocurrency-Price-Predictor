namespace Shared.Entities.Common
{
    public class CurrentUserEntity
    {
        public long Id { get; set; } 
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string LanguageCode { get; set; }
        public string Ip { get; set; }
        public string ExpiresDate { get; set; }
        public string Token { get; set; }

    }
}
