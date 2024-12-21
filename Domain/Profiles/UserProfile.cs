using AutoMapper;
using Domain.Abstractions.Records;
using Domain.Entities;

namespace Domain.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserRecord, LogInRequest>()
                .ConstructUsing(src => new LogInRequest(
                    src.Login, 
                    src.Email, 
                    src.Password));
            
            CreateMap<RegisterUserRecord, UserEntity>()
                .ForMember(dist => dist.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dist => dist.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<LogInRequest, RegisterUserRecord>()
                .ConstructUsing(src => new RegisterUserRecord(
                    src.Login, 
                    src.Email,
                    src.Password, 
                    "false"));

            CreateMap<UserEntity, UserInfoResponse>()
                .ConstructUsing(src => new UserInfoResponse(
                    src.Login, 
                    src.Email, 
                    src.IsAdmin));
        }
    }
}
