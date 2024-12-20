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
                .ForMember(dist => dist.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dist => dist.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dist => dist.Password, opt => opt.MapFrom(src => src.Password));
            
            CreateMap<RegisterUserRecord, UserEntity>()
                .ForMember(dist => dist.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dist => dist.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<LogInRequest, RegisterUserRecord>()
                .ForMember(dist => dist.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dist => dist.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dist => dist.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dist => dist.IsAdmin, opt => opt.MapFrom(src => "false"));

            CreateMap<UserEntity, UserInfoResponse>()
                .ForMember(dist => dist.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dist => dist.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dist => dist.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin));
        }
    }
}
