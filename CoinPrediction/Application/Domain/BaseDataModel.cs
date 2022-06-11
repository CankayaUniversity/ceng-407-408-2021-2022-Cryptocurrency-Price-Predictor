using System.ComponentModel.DataAnnotations;
using Shared.Entities.Common;

namespace Application.Domain
{
    public class BaseDataModel: BaseContextEntity
    {
        public long Id { get; set; }

        public DateTime CreateDate { get; set; }

        public long CreateUser { get; set; }

        public DateTime? UpdateDate { get; set; }

        public long? UpdateUser { get; set; }

       public byte Status { get; set; }
    }
}
