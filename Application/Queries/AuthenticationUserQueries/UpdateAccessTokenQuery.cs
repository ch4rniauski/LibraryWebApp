using MediatR;

namespace Application.Queries.AuthenticationUserQueries
{
    public class UpdateAccessTokenQuery : IRequest<string>
    {
        public Guid Id { get; set; }
        public string? RefreshToken { get; set; }
    }
}
