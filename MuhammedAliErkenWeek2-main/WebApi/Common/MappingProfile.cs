using AutoMapper;
using WebApi.Entities;
using WebApi.UserOperations.CreateUser;
using WebApi.UserOperations.GetUserDetail;
using WebApi.UserOperations.GetUsers;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserModel, User>();
            CreateMap<User, UserDetailViewModel>().ForMember(dest => dest.UserName,
                opt => opt.MapFrom(
                    src => src.FirstName + " " + src.LastName));
            CreateMap<User, UserViewModel>().ForMember(dest => dest.UserName,
                opt => opt.MapFrom(
                    src => src.FirstName + " " + src.LastName));
        }
    }
}