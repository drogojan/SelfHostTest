namespace SelfHostTest.API.Domain.Users
{
    public interface IUserRepository
    {
        User Add(User user);
        bool IsUsernameTaken(string username);
    }
}