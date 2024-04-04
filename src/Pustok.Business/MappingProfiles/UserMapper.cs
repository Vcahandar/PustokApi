using AutoMapper;
using Pustok.Business.DTOs.UserDtos;
using Pustok.Core.Entities.Identity;


namespace Pustok.Business.MappingProfiles
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserPostDto, AppUser>();
        }
    }
}
