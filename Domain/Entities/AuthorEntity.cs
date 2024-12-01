namespace Domain.Entities
{
    public class AuthorEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public List<BookEntity>? Books { get; set; }
    }
}
