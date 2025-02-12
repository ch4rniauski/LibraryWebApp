namespace Application.Abstractions.Records
{
    public record GetBookRecord(
        Guid Id,
        string ISBN,
        string Title,
        string Genre,
        string? AuthorFirstName,
        string? AuthorSecondName,
        string? Description,
        byte[]? ImageData,
        DateOnly? TakenAt,
        DateOnly? DueDate,
        Guid? UserId);
}