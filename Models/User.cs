namespace AuthCore_API.Models
{
    public sealed class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? RefreshTokenHash { get; set; } = null;
        public DateTime? RefreshTokenExpiryAt { get; set; } = null;
        public bool IsRefreshTokenRevoked { get; set; } = false;
    }
}
