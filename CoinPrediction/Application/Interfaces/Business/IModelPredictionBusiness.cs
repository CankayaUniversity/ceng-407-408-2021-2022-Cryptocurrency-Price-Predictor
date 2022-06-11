using Application.Domain;
using Entities.ModelPrediction;
using Shared.Entities.Common;

namespace Application.Interfaces.Business
{
    public interface IModelPredictionBusiness
    {
        public Task<ServiceResponse<ModelPrediction>> GetLastModelPrediction();

    }
}
