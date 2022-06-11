using System.Data;
using System.Net.Mail;
using Application.Api;
using Application.Domain;
using Application.Interfaces.Business;
using Application.Interfaces.Data;
using Application.Interfaces.Services;
using Application.Mapping;
using Data.Context;
using Entities.ModelPrediction;

using Shared.Entities.Common;
using Shared.Extentions;
using Shared.Utilities;

namespace Business
{
    public class ModelPredictionBusiness : IModelPredictionBusiness
    {
        #region Fields
        private readonly IRepository<ModelPrediction> _modelPredictionRepository;
        private readonly IDbContext _context;
        private readonly byte _activeStatus = Enums.TableStatus.Active.ToByte();
        #endregion

        #region Ctor
        public ModelPredictionBusiness(IRepository<ModelPrediction> modelPredictionRepository, IDbContext context)
        {
            _modelPredictionRepository = modelPredictionRepository;
            _context = context;
        }
        #endregion

        #region Methods

        public async Task<ServiceResponse<ModelPrediction>> GetLastModelPrediction()
        {
            return await Task.Run(() =>
            {
                using var tx = _context.BeginTransaction(IsolationLevel.ReadUncommitted).Result;
                try
                {
                    IQueryable<ModelPrediction> modelPredictionList = _modelPredictionRepository.GetListNoTracking();
                    if (!modelPredictionList.Any())
                    {
                        string? message = GetTranslationMessage("EmptyErrorMessage");
                        return new ServiceResponse<ModelPrediction>(Constants.ErrorCode, message ?? Constants.EmptyErrorMessage);
                    }

                    ModelPrediction result = modelPredictionList.OrderByDescending(x => x.Date).FirstOrDefault();

                    string? successMessage = GetTranslationMessage("TransactionSuccessfulMessage");
                    return new ServiceResponse<ModelPrediction>(result, successMessage);
                }
                catch (Exception ex)
                {
                    string? message = GetTranslationMessage("ExceptionErrorMessage");
                    return new ServiceResponse<ModelPrediction>(Constants.ErrorCode, message ?? Constants.ExceptionErrorMessage);
                }
            });
        }

        private string? GetTranslationMessage(string key)
        {
            string? message = null;
            string languageCode = ProjectConfiguration.CurrentUser.LanguageCode ?? Constants.TRLanguage;
            //var serviceResponse = _settingsApiIntegration.GetTranslations().Result;
            //if (serviceResponse.Result.ResultCode == Constants.SuccessCode)
            //    message = serviceResponse.ReturnObject.FirstOrDefault(x => x.Code == key && x.Culture == languageCode)?.Value;
            return message;
        }

        #endregion
    }
}
