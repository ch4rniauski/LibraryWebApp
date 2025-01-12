using Application.Abstractions.Requests;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles.AuthorProfiles
{
    public class CreateAuthorRecordToAuthorEntity : Profile
    {
        public CreateAuthorRecordToAuthorEntity()
        {
            CreateMap<CreateAuthorRecord, AuthorEntity>()
                .ForMember(dist => dist.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dist => dist.SecondName, opt => opt.MapFrom(src => src.SecondName))
                .ForMember(dist => dist.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dist => dist.BirthDate, opt => opt.MapFrom(src => src.BirthDate));
        }
    }
}
