using Amaris.Helpers.APIResponse;
using Amaris.Helpers.APIResponses;
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
        HttpClient client = new HttpClient();
        public EmployeeService(IOptions<ApiUrl> apiUrl)
        {
            _apiUrl = apiUrl.Value;
        }

        async Task<Response<Employee>> IBaseService<Employee>.GetItem(long id)
        {
            var response = new Response<Employee>();
            try
            {
                client.BaseAddress = new Uri(_apiUrl.BaseUrl!);
                var res = await client.GetAsync(_apiUrl.APIVersion + _apiUrl.EmployeeEndpoint + id.ToString());
                if (res.IsSuccessStatusCode)
                {
                    var results = res.Content.ReadAsStringAsync().Result;

                    var js = JsonConvert.DeserializeObject<GeneralAPIresponse>(results);
                    var data = EmployeeAPIMapper.MapEmployeeAPI(JsonConvert.DeserializeObject<EmployeeAPI>(js.data.ToString()));
                    response.Lst = new List<Employee>();
                    response.Lst.Add(data);

                }
                else
                {
                    response.Error = true;
                }
            }
            catch (Exception ex)
            {
                response.Error = true;
            }
            return response;
        }

        async Task<Response<Employee>> IBaseService<Employee>.GetItems(string param)
        {
            var response = new Response<Employee>();
            try
            {
                client.BaseAddress = new Uri(_apiUrl.BaseUrl!);
                var res = await client.GetAsync(_apiUrl.APIVersion + _apiUrl.EmployeesEndpoint);
                if (res.IsSuccessStatusCode)
                {
                    var results = res.Content.ReadAsStringAsync().Result;
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
                }
            }
            catch (Exception ex)
            {
                response.Error = true;
            }
            return response;
        }
    }
}
