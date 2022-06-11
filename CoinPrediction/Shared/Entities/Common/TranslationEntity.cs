namespace Shared.Entities.Common
{
    public class TranslationEntity
    {
        public long? Id { get; set; }

        public string Culture { get; set; }

        public string Code { get; set; }

        public string Value { get; set; }

        public string? Description { get; set; }

        public DateTime? CreateDate { get; set; }
        public long? CreateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUser { get; set; }
        public byte? Status { get; set; }


    }
}
