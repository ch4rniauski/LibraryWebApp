﻿namespace Domain.Abstractions.Records
{
    public record UpdateBookRecord(
        Guid Id,
        string ISBN,
        string Title,
        string? Genre,
        string? Description,
        string? AuthorFirstName,
        string? AuthorSecondName,
        DateOnly? TakenAt,
        DateOnly? DueDate);
}