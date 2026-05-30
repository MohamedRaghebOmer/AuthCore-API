namespace AuthCore_API.Models.DTOs.Requests
{
    public sealed record LoginRequestDto
    {
        public required string Username { get; init; } = null!;
        public required string Password { get; init; } = null!;
    }
}
