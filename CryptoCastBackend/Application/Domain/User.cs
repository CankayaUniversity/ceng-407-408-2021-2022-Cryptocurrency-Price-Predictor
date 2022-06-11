using System.ComponentModel.DataAnnotations;

namespace Application.Domain
{
    public class User: BaseDataModel
    {
        [StringLength(64)]
        public string UserName { get; set; }

        [StringLength(64)]
        public string Email { get; set; }

        [StringLength(64)]
        public string Password { get; set; }

        [StringLength(128)]
        public string? FullName { get; set; }

        [StringLength(16)]
        public string? Tckn { get; set; }

        [StringLength(32)]
        public string? PhoneNumber { get; set; }

        public DateTime? BirthDate { get; set; }

        public byte? Gender { get; set; }

        public int RoleId { get; set; }
        [StringLength(8)]
        public string? LanguageCode { get; set; }

    }
}
