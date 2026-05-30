namespace AuthCore_API.Models.DTOs.Responses
{
    public sealed record RefreshResponseDto
    {
        public required string AccessToken { get; init; }
        public required string RefreshToken { get; init; }
    }
}
