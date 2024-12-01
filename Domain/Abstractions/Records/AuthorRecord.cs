namespace Domain.Abstractions.Records
{
    public record AuthorRecord(Guid Id,
        string Name,
        string SecondName,
        string Country,
        DateOnly BirthDate);
}
