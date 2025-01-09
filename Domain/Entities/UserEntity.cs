namespace Domain.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public bool IsAdmin { get; set; } = false;
        public string Login {  get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? RefreshToken { get; set; } = null;
        public DateTime? RefreshTokenExpiresAt { get; set; } = null!;
        public List<BookEntity>? Books { get; set; }
    }
}
