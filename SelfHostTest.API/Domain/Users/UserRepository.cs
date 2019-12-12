using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using SelfHostTest.API.DbContexts;

namespace SelfHostTest.API.Domain.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public User Add(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return user;
        }

        public bool IsUsernameTaken(string username)
        {
            return dbContext.Users.Any(u => u.Username == username);
        }
    }
}