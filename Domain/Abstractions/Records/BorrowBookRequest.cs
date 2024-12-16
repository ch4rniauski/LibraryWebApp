namespace Domain.Abstractions.Records
{
    public record BorrowBookRequest(
        Guid UserId,
        Guid BookId);
}
