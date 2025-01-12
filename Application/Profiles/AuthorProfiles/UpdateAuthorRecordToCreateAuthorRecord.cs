using Application.Abstractions.Records;
using Application.Abstractions.Requests;
using AutoMapper;

namespace Application.Profiles.AuthorProfiles
{
    public class UpdateAuthorRecordToCreateAuthorRecord : Profile
    {
        public UpdateAuthorRecordToCreateAuthorRecord()
        {
            CreateMap<UpdateAuthorRecord, CreateAuthorRecord>()
                .ConstructUsing(src => new CreateAuthorRecord(
                    src.FirstName,
                    src.SecondName,
                    src.Country,
                    src.BirthDate));
        }
    }
}
