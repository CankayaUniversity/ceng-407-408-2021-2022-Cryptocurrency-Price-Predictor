using Shared.Entities.Common;

namespace Shared.ApiCall
{
    public interface IApiCall
    {
        public Task<TResponse> CallApi<TRequest, TResponse>(string route, TRequest requestModel, string clientName, int requestType = (int)Enums.ApiCallRequestType.POST);
        public Task<string> CallApi<TRequest>(string route, TRequest requestModel, string token, string clientName, int requestType = (int)Enums.ApiCallRequestType.POST);

    }
}
