using AutoMapper;
using Pustok.Business.DTOs.AuthorDtos;
using Pustok.Core.Entities;


namespace Pustok.Business.MappingProfiles
{
    public class AuthorMapper : Profile
    {
        public AuthorMapper()
        {
            CreateMap<AuthorPostDto, Author>().ReverseMap();
            CreateMap<Author, AuthorGetDto>().ReverseMap();
            CreateMap<AuthorPutDto, Author>().ReverseMap();
        }
    }
}
