using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace API.Employees.Test
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Startup>>
    {
        readonly HttpClient _client;

        public UnitTest1(WebApplicationFactory<Startup> application)
        {
            _client = application.CreateClient();
        }
      
        [Fact]
        public async Task GET_Employee()
        {
            var response = await _client.GetAsync("/api/Employee");
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
           
        }
    }
}
