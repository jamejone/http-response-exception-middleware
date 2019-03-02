using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using http_response_exception_middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace http_response_exception_middleware_tests
{
    public class ValuesControllerIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        protected readonly HttpClient _client;

        public ValuesControllerIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
        
        [Fact]
        public async Task Get_Returns_OhDear_BadRequest()
        {
            var response = await _client.GetAsync("/api/values");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal("{\"message\":\"Oh dear...\"}", responseContent);
        }
        
        [Fact]
        public async Task Get_Returns_Huh_NotFound()
        {
            var response = await _client.GetAsync("/api/values/15");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal("{\"message\":\"Huh?\"}", responseContent);
        }
        
        [Fact]
        public async Task Post_Returns_InternalServerError()
        {
            var values = new Dictionary<string, string>
            {
                { "value", "this-is-a-value" }
            };
            
            var requestContent = new FormUrlEncodedContent(values);

            var response = await _client.PostAsync("/api/values", requestContent);

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.Equal("{\"message\":\"internal server error :(\"}", responseContent);
        }
    }
}