﻿namespace Domain.Entities
{
    public class BookEntity
    {
        public Guid Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public Guid AuthorId { get; set; }
        public AuthorEntity Author { get; set; }
        public DateOnly? TakenAt { get; set; } = null;
        public DateOnly? DueDate { get; set; } = null;
    }
}
