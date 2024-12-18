namespace Domain.Abstractions.Records
{
    public record GetBookResponse(
        Guid Id,
        string Title,
        string? ImageURL);
}
