using Application.Abstractions.Requests;
using AutoMapper;

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
