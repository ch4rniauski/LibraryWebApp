namespace Application.Abstractions.Requests
{
    public record UpdateAuthorRecord(
        Guid Id,
        string FirstName,
        string SecondName,
        string Country,
        DateOnly BirthDate);
}
