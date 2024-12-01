namespace Domain.Abstractions.Records
{
    public record BookRecord(Guid Id,
        string ISBN,
        string Title,
        string? Genre,
        string? Description,
        string? AuthorName,
        DateOnly? TakenAt,
        DateOnly? DueDate);
}
