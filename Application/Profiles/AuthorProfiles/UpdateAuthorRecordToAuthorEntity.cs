using Application.Abstractions.Requests;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles.AuthorProfiles
{
    public class UpdateAuthorRecordToAuthorEntity : Profile
    {
        public UpdateAuthorRecordToAuthorEntity()
        {
            CreateMap<UpdateAuthorRecord, AuthorEntity>()
                .ForMember(dist => dist.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dist => dist.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dist => dist.SecondName, opt => opt.MapFrom(src => src.SecondName))
                .ForMember(dist => dist.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dist => dist.BirthDate, opt => opt.MapFrom(src => src.BirthDate));
        }
    }
}
