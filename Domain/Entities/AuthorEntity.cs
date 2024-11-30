namespace Domain.Entities
{
    public class AuthorEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Country { get; set; }
        public DateOnly BirthDate { get; set; }
        public List<BookEntity>? Books { get; set; }
    }
}
