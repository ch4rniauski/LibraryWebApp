namespace Domain.Abstractions.Records
{
    public record CreateAuthorRecord(
        string FirstName,
        string SecondName,
        string Country,
        DateOnly BirthDate);
}
