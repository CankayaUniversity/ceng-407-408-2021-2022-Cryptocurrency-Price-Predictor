namespace Entities.Common
{
    public class BaseEntity
    {
        public DateTime? CreateDate { get; set; }
        public long? CreateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUser { get; set; }
        public byte? Status { get; set; }
    }
}
