using SelfHostTest.API.Controllers;

namespace SelfHostTest.API.Domain.Users
{
    public interface IUserService
    {
        UserApiModel CreateUser(UserInputModel userInputModel);
    }
}