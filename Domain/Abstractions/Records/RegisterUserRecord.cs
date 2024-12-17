namespace Domain.Abstractions.Records
{
    public record RegisterUserRecord(
        string Login,
        string Email,
        string Password,
        bool IsAdmin);
}
