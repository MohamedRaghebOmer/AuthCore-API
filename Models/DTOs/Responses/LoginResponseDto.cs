namespace AuthCore_API.Models.DTOs.Responses
{
    public sealed record LoginResponseDto
    {
        public required string AccessToken { get; init; } = null!;
        public required string RefreshToken { get; init; } = null!;
    }
}
