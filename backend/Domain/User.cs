namespace TodoList.Domain;

public record User
{
    public Guid Id { get; init; }
    public string Email { get; set; } = String.Empty;
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }
    public string RefreshToken { get; set; } = String.Empty;
    public DateTime RefreshTokenCreated { get; set; }
    public DateTime RefreshTokenExpires { get; set; }
}

