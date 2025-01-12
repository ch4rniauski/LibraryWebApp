namespace Application.Abstractions.Requests
{
    public record CreateAuthorRecord(
        string FirstName,
        string SecondName,
        string Country,
        DateOnly BirthDate);
}
