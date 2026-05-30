namespace AuthCore_API.Models.DTOs.Requests
{
    public sealed record RefreshRequestDto
    {
        public required string Username { get; init; }
        public required string RefreshToken { get; init; }
    }
}
