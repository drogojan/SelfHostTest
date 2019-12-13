using AutoMapper;
using SelfHostTest.API.Domain.Users;
using SelfHostTest.API.Models;

namespace SelfHostTest.API.MappingConfigurations
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserInputModel, User>();
            CreateMap<User, UserApiModel>();
        }
    }
}
