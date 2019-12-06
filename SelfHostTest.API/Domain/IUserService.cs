using SelfHostTest.API.Controllers;

namespace SelfHostTest.API.Domain
{
    public interface IUserService
    {
        void CreateUser(UserInputModel userInputModel);
    }
}