﻿namespace Application.Abstractions.Requests
{
    public record CreateBookRecord(
        string ISBN,
        string Title,
        string Genre,
        string? Description,
        string? AuthorFirstName,
        string? AuthorSecondName,
        byte[]? ImageData,
        DateOnly? TakenAt,
        DateOnly? DueDate);
}
