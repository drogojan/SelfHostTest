using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using SelfHostTest.API;
using Xunit;

namespace SelfHostTest.AcceptanceTests
{
    public class WeatherForecastTests : IClassFixture<WebApplicationFactory<SelfHostTest.API.Startup>>
    {
        private readonly WebApplicationFactory<SelfHostTest.API.Startup> factory;

        public WeatherForecastTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

//        [Theory]
//        [InlineData("/weatherforecast")]
//        [InlineData("/gigi")]
//        [InlineData("/")]
//        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
//        {
//            // Arrange
//            var client = factory.CreateClient();
//
//            // Act
//            var response = await client.GetAsync(url);
//
//            // Assert
//            response.EnsureSuccessStatusCode(); // Status Code 200-299
//            Assert.Equal("application/json; charset=utf-8",
//                response.Content.Headers.ContentType.ToString());
//        }
    }
}
