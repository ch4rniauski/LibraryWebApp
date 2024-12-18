namespace Domain.Abstractions.Records
{
    public record LogInRequest(
        string Login,
        string Email,
        string Password);
}
