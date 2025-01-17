using Application.Abstractions.Records;
using MediatR;

namespace Application.Queries.UserQueries
{
    public class GetUserInfoQuery : IRequest<UserInfoResponse>
    {
        public Guid UserId { get; set; }
    }
}
