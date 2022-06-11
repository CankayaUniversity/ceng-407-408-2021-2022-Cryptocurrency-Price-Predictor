using System.Net;
using System.Text;
using System.Text.Json;
using Shared.Entities.Common;
using RestSharp;

namespace Shared.ApiCall
{
    public class ApiCall : IApiCall
    {
        #region Fileds

        private readonly IHttpClientFactory _clientFactory;

        #endregion

        #region Ctor
        public ApiCall(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        #endregion

        #region Methods

        public async Task<string> CallApi<TRequest>(string route, TRequest requestModel, string token, string clientName, int requestType = (int)Enums.ApiCallRequestType.POST)
        {
            try
            {
                var requestJson = JsonSerializer.Serialize(requestModel);
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                using (var client = _clientFactory.CreateClient(clientName))
                {

                    var stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
                    client.Timeout = TimeSpan.FromMinutes(15);
                    HttpResponseMessage response = new HttpResponseMessage();
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    switch (requestType)
                    {
                        case (int)Enums.ApiCallRequestType.GET:
                            response = client.GetAsync($"{client.BaseAddress}{route}").Result;
                            break;
                        case (int)Enums.ApiCallRequestType.POST:
                            response = client.PostAsync($"{client.BaseAddress}{route}", stringContent).Result;
                            break;
                        case (int)Enums.ApiCallRequestType.PUT:
                            response = client.PutAsync($"{client.BaseAddress}{route}", stringContent).Result;
                            break;
                        case (int)Enums.ApiCallRequestType.DELETE:
                            response = client.DeleteAsync($"{client.BaseAddress}{route}").Result;
                            break;
                    }

                    var responseModel = response.Content.ReadAsStringAsync().Result;
                    return responseModel;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<TResponse> CallApi<TRequest, TResponse>(string route, TRequest requestModel, string clientName, int requestType = (int)Enums.ApiCallRequestType.POST)
        {
            try
            {
                var requestJson = JsonSerializer.Serialize(requestModel);
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                using (var client = _clientFactory.CreateClient(clientName))
                {

                    var stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
                    client.Timeout = TimeSpan.FromMinutes(15);
                    HttpResponseMessage response = new HttpResponseMessage();
                    switch (requestType)
                    {
                        case (int)Enums.ApiCallRequestType.GET:
                            if (requestModel == null)
                                response = client.GetAsync($"{client.BaseAddress}{route}").Result;
                            else
                                response = client.GetAsync($"{client.BaseAddress}{route}/{requestModel}").Result;
                            break;
                        case (int)Enums.ApiCallRequestType.POST:
                            response = client.PostAsync($"{client.BaseAddress}{route}", stringContent).Result;
                            break;
                        case (int)Enums.ApiCallRequestType.PUT:
                            response = client.PutAsync($"{client.BaseAddress}{route}", stringContent).Result;
                            break;
                        case (int)Enums.ApiCallRequestType.DELETE:
                            response = client.DeleteAsync($"{client.BaseAddress}{route}").Result;
                            break;
                    }

                    var responseModel1 = response.Content.ReadAsByteArrayAsync().Result;
                    var bytstr = String.Join(" ", responseModel1);
                    string str = Encoding.Default.GetString(responseModel1);

                    var responseModel = response.Content.ReadAsStreamAsync().Result;

                    var result = await JsonSerializer.DeserializeAsync<TResponse>(responseModel);
                    return result;
                    //return await JsonSerializer.DeserializeAsync<TResponse>(null);

                }
            }
            catch (Exception ex)
            {
                return await JsonSerializer.DeserializeAsync<TResponse>(null);
            }
        }


        #endregion

    }
}
