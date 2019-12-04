using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AspNetCore.Http.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using SelfHostTest.API;
using Xunit;

namespace SelfHostTest.AcceptanceTests
{
    public class RegistrationTests : IClassFixture<WebApplicationFactory<SelfHostTest.API.Startup>>
    {
        private readonly WebApplicationFactory<SelfHostTest.API.Startup> factory;

        public RegistrationTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task Register_a_new_user()
        {
            var client = factory.CreateClient();
            var user = new
            {
                username = "alice",
                password = "alis123",
                about = "About Alice"
            };
            var response = await client.PostAsJsonAsync("/users", user);
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var actual = await response.Content.ReadAsJsonAsync<JObject>();

            actual.GetValue("id").ToObject<int>().Should().BePositive();
            actual.GetValue("username").ToObject<string>().Should().Be("alice");
            actual.GetValue("about").ToObject<string>().Should().Be("About Alice");
        }
    }
}
