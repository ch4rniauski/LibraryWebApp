using Application.Abstractions.Requests;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles.UserProfiles
{
    public class RegisterUserRecordToUserEntity : Profile
    {
        public RegisterUserRecordToUserEntity()
        {
            CreateMap<RegisterUserRecord, UserEntity>()
                .ForMember(dist => dist.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dist => dist.IsAdmin, opt => opt.Ignore())
                .ForMember(dist => dist.PasswordHash, opt => opt.Ignore())
                .ForMember(dist => dist.Email, opt => opt.MapFrom(src => src.Email));
        }
    }
}
