using Amaris.Helpers.APIResponse;
using Amaris.Helpers.Configs;
using Amaris.Helpers.Facades;
using Amaris.Helpers.Mappers;
using Amaris.Models.DTO;
using Amaris.Models.General;
using Amaris.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Amaris.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApiUrl _apiUrl;
        private readonly ILogger<EmployeeService> _logger;
        private readonly HttpClient _client;

        public EmployeeService(IOptions<ApiUrl> apiUrl, ILogger<EmployeeService> logger)
        {
            _apiUrl = apiUrl.Value;
            _logger = logger;
            _client = new HttpClient();
        }

        async Task<Response<Employee>> IBaseService<Employee>.GetItem(long id)
        {
            var response = new Response<Employee>();
            try
            {
                _client.BaseAddress = new Uri(_apiUrl.BaseUrl!);
                var res = await _client.GetAsync(_apiUrl.APIVersion + _apiUrl.EmployeeEndpoint + id.ToString());
                if (res.IsSuccessStatusCode)
                {
                    var results = await res.Content.ReadAsStringAsync();
                    var js = JsonConvert.DeserializeObject<GeneralAPIresponse>(results);
                    var data = EmployeeAPIMapper.MapEmployeeAPI(JsonConvert.DeserializeObject<EmployeeAPI>(js.data.ToString()));
                    response.Lst = new List<Employee>();
                    response.Lst.Add(data);
                }
                else
                {
                    response.Error = true;
                    _logger.LogError( "An error occurred while getting the employee item From de API.");
                    response.Msg = res.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the employee item.");
                response.Msg = ex.Message;
                response.Error = true;
            }
            return response;
        }

        async Task<Response<Employee>> IBaseService<Employee>.GetItems(string param)
        {
            var response = new Response<Employee>();
            try
            {
                _client.BaseAddress = new Uri(_apiUrl.BaseUrl!);
                var res = await _client.GetAsync(_apiUrl.APIVersion + _apiUrl.EmployeesEndpoint);
                if (res.IsSuccessStatusCode)
                {
                    var results = await res.Content.ReadAsStringAsync();
                    var js = JsonConvert.DeserializeObject<GeneralAPIresponse>(results);
                    var data = JsonConvert.DeserializeObject<List<EmployeeAPI>>(js.data.ToString());
                    response.Lst = new List<Employee>();
                    foreach (var item in data)
                    {
                        response.Lst.Add(EmployeeAPIMapper.MapEmployeeAPI(item));
                    }
                }
                else
                {
                    response.Error = true;
                    _logger.LogError("An error occurred while getting the employee items From de API.");
                    response.Msg = res.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the employee items.");
                response.Msg = ex.Message;
                response.Error = true;
            }
            return response;
        }
    }
}
