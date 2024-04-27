using AutoMapper;
using Kampus.Model.Entities;
using Kampus.Model.Entities.Dto;

namespace Kampus.BI.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Professor, ProfessorDto>();
            CreateMap<ProfessorDto, Professor>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
            CreateMap<University, UniversityDto>();
            CreateMap<UniversityDto, University>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}