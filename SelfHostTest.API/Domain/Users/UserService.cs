using System;
using SelfHostTest.API.Models;

namespace SelfHostTest.API.Domain.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserApiModel CreateUser(UserInputModel userInputModel)
        {
            if (userRepository.IsUsernameTaken(userInputModel.Username))
                throw new UsernameAlreadyInUseException();

            User user = CreateUserFrom(userInputModel);

            User createdUser = userRepository.Add(user);

            return CreateUserApiModelFrom(createdUser);
        }

        private static UserApiModel CreateUserApiModelFrom(User createdUser)
        {
            return new UserApiModel { Id = createdUser.Id, Username = createdUser.Username, About = createdUser.About };
        }

        private static User CreateUserFrom(UserInputModel userInputModel)
        {
            return new User { Username = userInputModel.Username, Password = userInputModel.Password, About = userInputModel.About };
        }
    }
}