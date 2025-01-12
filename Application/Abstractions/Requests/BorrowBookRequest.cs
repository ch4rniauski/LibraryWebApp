namespace Application.Abstractions.Records
{
    public record BorrowBookRequest(
        Guid UserId,
        Guid BookId);
}
