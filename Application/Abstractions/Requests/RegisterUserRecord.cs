namespace Application.Abstractions.Requests
{
    public record RegisterUserRecord(
        string Login,
        string Email,
        string Password,
        string IsAdmin);
}
