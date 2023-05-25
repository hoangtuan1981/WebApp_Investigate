using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp_Investigate;
using Xunit;

namespace IntegrationTestWithXUnit
{
    public class HomeControllerIntergrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public HomeControllerIntergrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Test_HomeController_Return_OK()
        {
            // Arrange
            var defaultPage = await _client.GetAsync("/");

            Assert.NotNull(defaultPage);
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
        }
    }
}
