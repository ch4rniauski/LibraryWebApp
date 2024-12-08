namespace Domain.Abstractions.Records
{
    public record UpdateAuthorRecord(
        Guid Id,
        string FirstName,
        string SecondName,
        string Country,
        DateOnly BirthDate);
}
