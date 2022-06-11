using Shared.Entities.Common;

namespace Application.Interfaces.Services
{
    public interface ISettingsApiIntegration
    {
        public Task<ServiceResponse<List<TranslationEntity>>> GetTranslations();
    }
}
