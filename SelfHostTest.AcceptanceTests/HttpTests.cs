using System;
using System.Net.Http;
using Xunit;
using Xunit.Abstractions;

namespace SelfHostTest.AcceptanceTests
{
    public class HttpTests : IClassFixture<TestFixture>, IDisposable
    {
        readonly TestFixture _fixture;
        readonly HttpClient _client;

        public HttpTests(TestFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            fixture.Output = output;
            _client = fixture.CreateClient();
        }

        public void Dispose() => _fixture.Output = null;
    }
}