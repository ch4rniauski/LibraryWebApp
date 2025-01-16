using Application.Abstractions.Records;
using Application.Abstractions.Requests;
using MediatR;

namespace Application.Queries.AuthenticationUserQueries
{
    public class LogInUserQuery : IRequest<LogInResponseRecord>
    {
        public LogInRequest User { get; set; } = null!;
    }
}
