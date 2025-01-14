using Application.Abstractions.Requests;
using AutoMapper;

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
