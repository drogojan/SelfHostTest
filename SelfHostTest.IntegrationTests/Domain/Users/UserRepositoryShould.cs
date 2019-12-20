using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SelfHostTest.API.DbContexts;
using SelfHostTest.API.Domain.Users;
using Xunit;

namespace SelfHostTest.IntegrationTests.Domain.Users
{
    public class UserRepositoryShould
    {
        [Fact]
        public void Inform_when_a_username_is_already_taken()
        {
            DbContextOptionsBuilder<ApplicationDbContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
//            dbContextOptionsBuilder.UseInMemoryDatabase("InMemoryDbForTesting");
            dbContextOptionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=OutsideInTdd-Test-DB;Trusted_Connection=True;");
            DbContextOptions<ApplicationDbContext> options = dbContextOptionsBuilder.Options;

            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.Migrate();
//            dbContext.Database.EnsureCreated();

            UserRepository sut = new UserRepository(dbContext);
            User ALICE = new User { Username = "Alice", Password = "Alice123", About = "About Alice" };
            User CHARLIE = new User { Username = "Charley", Password = "charl13", About = "About Charlie" };

            sut.Add(ALICE);

            sut.IsUsernameTaken(ALICE.Username).Should().BeTrue();
            sut.IsUsernameTaken(CHARLIE.Username).Should().BeFalse();
        }
    }
}
