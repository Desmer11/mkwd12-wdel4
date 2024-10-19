using AutoMapper;
using Lamazon.Domain.Entities;
using Lamazon.ViewModels.Models;

namespace Lamazon.Services.AutoMapperProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, RegisterUserViewModel>().ReverseMap();
        }
    }
}
