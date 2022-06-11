using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ModelPrediction
{
    public class ModelPredictionEntity : BaseEntity
    {
        public long? Id { get; set; }
        public int? Prediction { get; set; }
        public DateTime Date { get; set; }
    }
}
