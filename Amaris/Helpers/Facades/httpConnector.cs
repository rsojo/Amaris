using Amaris.Helpers.Configs;
using Amaris.Services.Implementations;
using Microsoft.Extensions.Options;

namespace Amaris.Helpers.Facades
{
    public class httpConnector
    {
        private readonly ApiUrl _apiUrl;
        private readonly ILogger<httpConnector> _logger;
        private readonly HttpClient _client;

        public httpConnector(IOptions<ApiUrl> apiUrl, ILogger<httpConnector> logger)
        {
            _apiUrl = apiUrl.Value;
            _logger = logger;
            _client = new HttpClient();
        }

        public async Task<HttpResponseMessage> APIConnect(string url)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await _client.GetAsync(_apiUrl.APIVersion + _apiUrl.EmployeeEndpoint + url.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while connecting to the API.");
            }
            return response;
        }
    }
}
