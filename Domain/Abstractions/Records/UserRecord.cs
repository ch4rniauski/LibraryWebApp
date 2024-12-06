namespace Domain.Abstractions.Records
{
    public record UserRecord(
        string Login,
        string Email,
        string Password);
}
