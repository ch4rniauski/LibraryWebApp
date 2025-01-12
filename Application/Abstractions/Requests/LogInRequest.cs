namespace Application.Abstractions.Requests
{
    public record LogInRequest(
        string Login,
        string Email,
        string Password);
}
