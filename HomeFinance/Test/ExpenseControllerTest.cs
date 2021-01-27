using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using WebApi;
using System.Net.Http;
using System.Net;

namespace Test
{
    public class ExpenseControllerTest
    {
        private readonly HttpClient _client;

        public ExpenseControllerTest()
        {
            var server = new TestServer(new WebHostBuilder().UseEnvironment("Development").UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Theory]
        [InlineData("GET")]
        [InlineData("GET", 1)]
        [InlineData("GET", 6)]
        public async Task ExpenseGetAllTestAsync(string method, int? id = null)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"/api/expense/{id}");

            //Act
            var responce = await _client.SendAsync(request);

            //Assert
            responce.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, responce.StatusCode);
        }
        [Theory]
        [InlineData("Delete", 5)]
        [InlineData("Delete", 4)]
        public async Task DeleteExpenseTestAsync(string method, int id)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"/api/expense/{id}");

            //Act
            var responce = await _client.SendAsync(request);

            //Assert
            responce.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, responce.StatusCode);
        }
    }
}
