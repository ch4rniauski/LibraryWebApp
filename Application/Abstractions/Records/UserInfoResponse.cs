namespace Application.Abstractions.Records
{
    public record UserInfoResponse(
        string Login,
        string Email,
        bool IsAdmin);
}
