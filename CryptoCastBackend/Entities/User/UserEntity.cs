using Entities.Common;

namespace Entities.User
{
    public class UserEntity:BaseEntity
    {
        public long? Id { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Tckn { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte? Gender { get; set; }
        public int? RoleId { get; set; }
        public string? LanguageCode { get; set; }
        
    }
}
