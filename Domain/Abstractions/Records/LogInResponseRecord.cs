namespace Domain.Abstractions.Records
{
    public record LogInResponseRecord(
        Guid Id,
        string AccessToken,
        string RefreshToken);
}
