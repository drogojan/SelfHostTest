using System;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using SelfHostTest.API.DbContexts;
using SelfHostTest.API.Domain.Users;
using Xunit;
using Xunit.Abstractions;

namespace SelfHostTest.UnitTests.Domain.Users
{
    public class UserRepositoryShould
    {
        [Fact]
        public void Inform_when_a_username_is_already_taken()
        {
            User ALICE = new User { Username = "Alice", Password = "Alis", About = "About Alice" };
            User CHARLIE = new User { Username = "Charlie", Password = "Charli", About = "About Charlie" };

            var dbContext = CreateInMemoryApplicationDbContext();

            UserRepository sut = new UserRepository(dbContext);

            sut.Add(ALICE);

            sut.IsUsernameTaken(ALICE.Username).Should().BeTrue();
            sut.IsUsernameTaken(CHARLIE.Username).Should().BeFalse();
        }

        private static ApplicationDbContext CreateInMemoryApplicationDbContext()
        {
            DbContextOptionsBuilder<ApplicationDbContext> dbContextOptionsBuilder =
                new DbContextOptionsBuilder<ApplicationDbContext>();
            dbContextOptionsBuilder.UseInMemoryDatabase("OutsideInTdd-InMemoryTest-DB");
            DbContextOptions<ApplicationDbContext> options = dbContextOptionsBuilder.Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            return dbContext;
        }
    }
}