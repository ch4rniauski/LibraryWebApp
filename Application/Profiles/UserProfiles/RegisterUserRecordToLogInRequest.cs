using Application.Abstractions.Requests;
using AutoMapper;
using Domain.Abstractions.Records;

namespace Application.Profiles.UserProfiles
{
    public class RegisterUserRecordToLogInRequest : Profile
    {
        public RegisterUserRecordToLogInRequest()
        {
            CreateMap<RegisterUserRecord, LogInRequest>()
                .ConstructUsing(src => new LogInRequest(
                    src.Login,
                    src.Email,
                    src.Password));
        }
    }
}
