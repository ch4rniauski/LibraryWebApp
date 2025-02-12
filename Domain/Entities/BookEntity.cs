namespace Domain.Entities
{
    public class BookEntity
    {
        public Guid Id { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid? AuthorId { get; set; } = null;
        public AuthorEntity? Author { get; set; }
        public DateOnly? TakenAt { get; set; } = null;
        public DateOnly? DueDate { get; set; } = null;
        public UserEntity? User { get; set; } = null;
        public Guid? UserId { get; set; } = null;
        public byte[]? ImageData { get; set; } = null;
    }
}
