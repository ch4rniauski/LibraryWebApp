namespace Application.Abstractions.Records
{
    public record GetBookResponse(
        Guid Id,
        string Title,
        byte[]? ImageData);
}
