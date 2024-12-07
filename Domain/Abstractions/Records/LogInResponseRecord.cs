namespace Domain.Abstractions.Records
{
    public record LogInResponseRecord(
        string AccessToken,
        string RefreshToken);
}
