namespace Application.Abstractions.Records
{
    public record GetBookRecord(
        Guid Id,
        string ISBN,
        string Title,
        string Genre,
        string? Description,
        string? AuthorFirstName,
        string? AuthorSecondName,
        byte[]? ImageData,
        DateOnly? TakenAt,
        DateOnly? DueDate,
        Guid? UserId);
}