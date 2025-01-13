namespace Application.Abstractions.Requests
{
    public record GetBookRequest(
        string Search,
        string SortBy);
}
