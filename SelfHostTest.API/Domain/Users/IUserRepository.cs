namespace SelfHostTest.API.Domain.Users
{
    public interface IUserRepository
    {
        User Create(User user);
        bool IsUsernameTaken(string username);
    }
}