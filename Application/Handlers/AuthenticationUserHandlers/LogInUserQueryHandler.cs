using Application.Abstractions.Records;
using Application.Abstractions.UseCases.AuthenticationUserUseCases;
using Application.Queries.AuthenticationUserQueries;
using MediatR;

namespace Application.Handlers.AuthenticationUserHandlers
{
    public class LogInUserQueryHandler : IRequestHandler<LogInUserQuery, LogInResponseRecord>
    {
        private readonly ILogInUserUseCase _logInUserUseCase;

        public LogInUserQueryHandler(ILogInUserUseCase logInUserUseCase)
        {
            _logInUserUseCase = logInUserUseCase;
        }

        public async Task<LogInResponseRecord> Handle(LogInUserQuery request, CancellationToken cancellationToken)
        {
            var response = await _logInUserUseCase.Execute(request.User);

            return response;
        }
    }
}
