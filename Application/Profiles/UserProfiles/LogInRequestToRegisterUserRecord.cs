using Application.Abstractions.Requests;
using AutoMapper;
using Domain.Abstractions.Records;

namespace Application.Profiles.UserProfiles
{
    public class LogInRequestToRegisterUserRecord : Profile
    {
        public LogInRequestToRegisterUserRecord()
        {
            CreateMap<LogInRequest, RegisterUserRecord>()
                .ConstructUsing(src => new RegisterUserRecord(
                    src.Login,
                    src.Email,
                    src.Password,
                    "false"));
        }
    }
}
