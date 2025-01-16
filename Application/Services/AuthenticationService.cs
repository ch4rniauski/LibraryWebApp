using Application.Abstractions.Records;
using Application.Abstractions.Requests;
using Application.Abstractions.Services;
using Application.Commands.AuthenticationUserCommands;
using Application.Queries.AuthenticationUserQueries;
using MediatR;

namespace Application.Services
{
    public class AuthenticationUserService : IAuthenticationUserService
    {
        private readonly IMediator _mediator;

        public AuthenticationUserService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task DeleteUser(Guid id)
        {
            var command = new DeleteUserCommand { UserId = id };

            await _mediator.Send(command);
        }

        public async Task<LogInResponseRecord> LogInUser(LogInRequest user)
        {
            var query = new LogInUserQuery { User = user };

            var response = await _mediator.Send(query);
            
            return response;
        }

        public async Task RegisterUser(RegisterUserRecord user)
        {            
            var command = new RegisterUserCommand { User = user };

            await _mediator.Send(command);
        }

        public async Task<string> UpdateAccessToken(Guid id, string? refreshToken)
        {
            var query = new UpdateAccessTokenQuery {
                Id = id,
                RefreshToken = refreshToken
            };

            var accessToken = await _mediator.Send(query);

            return accessToken;
        }
    }
}
