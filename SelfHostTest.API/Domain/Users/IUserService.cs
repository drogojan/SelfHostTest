using SelfHostTest.API.Controllers;

namespace SelfHostTest.API.Domain.Users
{
    public interface IUserService
    {
        UserViewModel CreateUser(UserInputModel userInputModel);
    }
}