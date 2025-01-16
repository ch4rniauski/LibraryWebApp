namespace Application.Abstractions.Records
{
    public record LogInResponseRecord(
        Guid Id,
        string AccessToken,
        string RefreshToken,
        bool IsAdmin);
}
