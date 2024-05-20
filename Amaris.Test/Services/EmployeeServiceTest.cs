
using Amaris.Helpers.APIResponse;
using Amaris.Helpers.Configs;
using Amaris.Helpers.Facades;
using Amaris.Services.Implementations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;

namespace Amaris.Test.Services
{
    public class EmployeeServiceTest
    {
        private readonly Mock<IOptions<ApiUrl>> _apiUrl;
        private readonly Mock<ILogger<EmployeeService>> _logger;
        private readonly Mock<HttpClient> _client;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTest()
        {
            _apiUrl = new Mock<IOptions<ApiUrl>>();
            _logger = new Mock<ILogger<EmployeeService>>();
            _client = new Mock<HttpClient>();
            _employeeService = new EmployeeService(_apiUrl.Object, _logger.Object);
        }

        [Fact]
        public async Task GetItem_WhenCalled_ReturnsEmployee()
        {
            // Arrange
            var id = 1;
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(httpMessageHandlerMock.Object);

            var employeeAPI = new EmployeeAPI
            {
                id = 1,
                employee_name = "test",
                employee_salary = 1000,
                employee_age = 20,
                profile_image = "test.jpg"
            };
            var generalAPIresponse = new GeneralAPIresponse
            {
                status = "success",
                data = employeeAPI
            };
            
            httpMessageHandlerMock
              .Protected()
              .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
              .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
              {
                  HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                  {
                      Content = new StringContent(JsonConvert.SerializeObject(generalAPIresponse))
                  };

                  return response;
              });
            //_client.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(response);
            _apiUrl.Setup(x => x.Value).Returns(new ApiUrl { APIVersion = "v1", EmployeeEndpoint = "employee/", EmployeesEndpoint = "employees/" });

            // Act
            var result = await _employeeService.GetItem(id);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Error);
            Assert.Equal(1, result.Lst.Count);
            Assert.Equal(id, result.Lst[0].Id);
            Assert.Equal("test", result.Lst[0].Name);
            Assert.Equal(1000, result.Lst[0].Salary);
            Assert.Equal(20, result.Lst[0].Age);
            Assert.Equal("test.jpg", result.Lst[0].ProfileImage);
        }
    }
}
