namespace Application.Abstractions.Requests
{
    public record BorrowBookRequest(
        Guid UserId,
        Guid BookId);
}
