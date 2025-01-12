using AutoMapper;
using Domain.Abstractions.Records;
using Domain.Entities;

namespace Application.Profiles.UserProfiles
{
    public class UserEntityToUserInfoResponse : Profile
    {
        public UserEntityToUserInfoResponse()
        {
            CreateMap<UserEntity, UserInfoResponse>()
                .ConstructUsing(src => new UserInfoResponse(
                    src.Login,
                    src.Email,
                    src.IsAdmin));
        }
    }
}
