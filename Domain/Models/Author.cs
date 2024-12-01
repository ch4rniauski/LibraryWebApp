namespace Domain.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }

        public Author(Guid id, string name, string secondName, string country, DateOnly birthDate)
        {
            Id = id;
            Name = name;
            SecondName = secondName;
            Country = country;
            BirthDate = birthDate;
        }
    }
}
