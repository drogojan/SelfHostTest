using System.Net;
using System.Threading.Tasks;
using AspNetCore.Http.Extensions;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;

namespace SelfHostTest.AcceptanceTests
{
    public class RegistrationApiTests : IClassFixture<TestFixture>
    {
        private readonly TestFixture fixture;

        public RegistrationApiTests(TestFixture fixture, ITestOutputHelper output)
        {
            this.fixture = fixture;
            fixture.Output = output;
        }

        [Fact]
        public async Task Register_a_new_user()
        {
            var client = fixture.CreateClient();
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
