using System;
using System.Net.Mime;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SelfHostTest.API.DbContexts;
using SelfHostTest.API.Domain.Users;
using Xunit;

namespace SelfHostTest.IntegrationTests
{
    public class UserRepositoryShould
    {
        [Fact]
        public void Inform_when_a_username_is_already_taken()
        {
            DbContextOptionsBuilder<ApplicationDbContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            dbContextOptionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=OutsideInTddDB;Trusted_Connection=True;");
            DbContextOptions<ApplicationDbContext> options = dbContextOptionsBuilder.Options;

            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.Migrate();

            UserRepository sut = new UserRepository(dbContext);
            User ALICE = new User { Username = "Alice", Password = "Alice123", About = "About Alice" };

            sut.Add(ALICE);

            sut.IsUsernameTaken(ALICE.Username).Should().BeTrue();
        }
    }
}
