using Application.Abstractions.Records;
using Application.Abstractions.Services;
using Application.Commands.UserCommands;
using Application.Queries.UserQueries;
using MediatR;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMediator _mediator;

        public UserService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task BorrowBook(Guid userId, Guid bookId)
        {
            var command = new BorrowBookCommand 
            { 
                BookId = bookId,
                UserId = userId,
            };

            await _mediator.Send(command);
        }

        public async Task<UserInfoResponse> GetUserInfo(Guid id)
        {
            var query = new GetUserInfoQuery { UserId = id };

            var userInfo = await _mediator.Send(query);

            return userInfo;
        }
    }
}
