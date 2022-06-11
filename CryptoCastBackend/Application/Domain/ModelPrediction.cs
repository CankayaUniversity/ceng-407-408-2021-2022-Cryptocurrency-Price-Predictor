using System.ComponentModel.DataAnnotations;

namespace Application.Domain
{
    public class ModelPrediction : BaseDataModel
    {
        public int Prediction { get; set; }
        public DateTime Date { get; set; }

    }
}
