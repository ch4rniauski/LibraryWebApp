using AutoMapper;
using Domain.Abstractions.Records;
using Domain.Entities;

namespace Domain.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<UpdateAuthorRecord, CreateAuthorRecord>()
                .ConstructUsing(src => new CreateAuthorRecord(
                    src.FirstName,
                    src.SecondName,
                    src.Country,
                    src.BirthDate));

            CreateMap<CreateAuthorRecord, AuthorEntity>()
                .ForMember(dist => dist.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dist => dist.SecondName, opt => opt.MapFrom(src => src.SecondName))
                .ForMember(dist => dist.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dist => dist.BirthDate, opt => opt.MapFrom(src => src.BirthDate));

            CreateMap<AuthorEntity, CreateAuthorRecord>()
                .ConstructUsing(src => new CreateAuthorRecord(
                    src.FirstName,
                    src.SecondName,
                    src.Country,
                    src.BirthDate));
        }
    }
}
