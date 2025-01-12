using Application.Abstractions.Requests;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles.AuthorProfiles
{
    public class AuthorEntityToCreateAuthorRecord : Profile
    {
        public AuthorEntityToCreateAuthorRecord()
        {
            CreateMap<AuthorEntity, CreateAuthorRecord>()
                .ConstructUsing(src => new CreateAuthorRecord(
                    src.FirstName,
                    src.SecondName,
                    src.Country,
                    src.BirthDate));
        }
    }
}
