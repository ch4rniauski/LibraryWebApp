namespace Domain.Abstractions.Records
{
    public record GetBookRequest(
        string Search,
        string SortBy);
}
