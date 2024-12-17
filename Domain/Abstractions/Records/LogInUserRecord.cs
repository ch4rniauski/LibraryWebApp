namespace Domain.Abstractions.Records
{
    public record LogInUserRecord(
        string Login,
        string Email,
        string Password);
}
