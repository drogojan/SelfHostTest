using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SelfHostTest.API;
using SelfHostTest.API.DbContexts;
using Xunit.Abstractions;

namespace SelfHostTest.AcceptanceTests
{
    public class TestFixture : WebApplicationFactory<Startup>
    {
        // Must be set in each test
        public ITestOutputHelper Output { get; set; }

        protected override IHostBuilder CreateHostBuilder()
        {
            var builder = base.CreateHostBuilder();
            builder.ConfigureLogging(logging =>
            {
                logging.ClearProviders(); // Remove other loggers
                logging.AddXUnit(Output); // Use the ITestOutputHelper instance
            });

            return builder;
        }


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");

            builder.ConfigureAppConfiguration(configurationBuilder => { configurationBuilder.AddJsonFile(configPath); });

            builder.ConfigureServices(services =>
            {
                var buildServiceProvider = services.BuildServiceProvider();

                using (var scope = buildServiceProvider.CreateScope())
                {
                    var scopeServiceProvider = scope.ServiceProvider;
                    var dbContext = scopeServiceProvider.GetRequiredService<ApplicationDbContext>();
                    dbContext.Database.EnsureDeleted();
                    dbContext.Database.Migrate();
                }
            });

            // Don't run IHostedServices when running as a test
            //builder.ConfigureTestServices((services) =>
            //{
            //    services.RemoveAll(typeof(IHostedService));
            //});
        }
    }
}
