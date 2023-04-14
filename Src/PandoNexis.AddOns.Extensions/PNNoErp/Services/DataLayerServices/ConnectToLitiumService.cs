using Litium.Accelerator.Utilities;
using Litium.Runtime.DependencyInjection;
using PandoNexis.AddOns.Extensions.PNNoErp.Constants;
using System.Net;
using System.Text;

namespace PandoNexis.AddOns.Extensions.PNNoErp.Services.DataLayerServices
{
    [Service(ServiceType = typeof(ConnectToLitiumService))]
    public class ConnectToLitiumService
    {
        private readonly PersonStorage _personStorage;


        public ConnectToLitiumService(PersonStorage personStorage)
        {
            _personStorage = personStorage;
           
        }

        public async Task<string> GetData(string method, Dictionary<string, string> parameters)
        {
            if (_personStorage?.CurrentSelectedOrganization == null) return null;
            
            var baseUrl = _personStorage.CurrentSelectedOrganization.Fields.GetValue<string>(NoErpOrderAdminConstants.BaseUrl);
            var token = _personStorage.CurrentSelectedOrganization.Fields.GetValue<string>(NoErpOrderAdminConstants.Authorization);
            
            var url = baseUrl + method;

            if (parameters != null && parameters.Any())
            {
                foreach (var value in parameters)
                {
                    url += "?" + value.Key + "=" + value.Value;
                }
            }
            try
            {
                var _client = new HttpClient();
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Add(NoErpOrderAdminConstants.Authorization, token);

                var response = await _client.GetAsync(url);
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.Unauthorized) // Todo Handle If Status Not Success.
                {
                    var tt = response.StatusCode;
                }

                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode &&
                        !string.IsNullOrWhiteSpace(responseString))
                {
                    return responseString;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("cannot connect");
            }
        }
        public async Task<string> PostData(string method, string jsonData, Dictionary<string, string> parameters)
        {
            var baseUrl = _personStorage.CurrentSelectedOrganization.Fields.GetValue<string>(NoErpOrderAdminConstants.BaseUrl);
            var token = _personStorage.CurrentSelectedOrganization.Fields.GetValue<string>(NoErpOrderAdminConstants.Authorization);
            var url = baseUrl + method;

            if (parameters != null && parameters.Any())
            {
                foreach (var value in parameters)
                {
                    url += "?" + value.Key + "=" + value.Value;
                }
            }
            try
            {

                var _client = new HttpClient();
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Add(NoErpOrderAdminConstants.Authorization, token);

                var response = await _client.PostAsync(url, new StringContent(jsonData, Encoding.UTF8, "application/json"));
                var responseString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.Unauthorized) // Todo Handle If Status Not Success.
                {
                    var tt = response.StatusCode;
                }


                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode &&
                        !string.IsNullOrWhiteSpace(responseString))
                {
                    return responseString;
                }
                return null;
            }
            catch (Exception e)
            {

                throw new Exception("cannot connect");
            }


        }
    }
}
