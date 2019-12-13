using AutoMapper;
using SelfHostTest.API.Models;

namespace SelfHostTest.API.Domain.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public UserApiModel CreateUser(UserInputModel userInputModel)
        {
            if (userRepository.IsUsernameTaken(userInputModel.Username))
                throw new UsernameAlreadyInUseException();

            User user = mapper.Map<User>(userInputModel);

            User createdUser = userRepository.Add(user);

            return mapper.Map<UserApiModel>(createdUser);
        }
    }
}