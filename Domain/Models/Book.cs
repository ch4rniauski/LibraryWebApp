namespace Domain.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Genre { get; set; }
        public string? Description { get; set; }
        public string? AuthorName { get; set; }
        public DateOnly? TakenAt { get; set; } = null;
        public DateOnly? DueDate { get; set; } = null;
    }
}
