using AuthCore_API.Data;
using AuthCore_API.Models;
using AuthCore_API.Models.DTOs.Requests;
using AuthCore_API.Models.DTOs.Responses;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthCore_API.Services
{
    public static class AuthService
    {
        public static LoginResponseDto? Login(LoginRequestDto loginRequest)
        {
            if (loginRequest == null
                || string.IsNullOrWhiteSpace(loginRequest.Username)
                || string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                return null;
            }

            var user = UsersData.Users.FirstOrDefault(
                u => u.Username == loginRequest.Username.Trim());

            if (user == null) return null;

            var canLogin = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash);

            if (!canLogin) return null;

            var refreshToken = GenerateRefreshToken();
            var accessToken = GenerateAccessToken(user);

            // Update the user with the new refresh token and its expiry time
            user.RefreshTokenHash = BCrypt.Net.BCrypt.HashPassword(refreshToken);
            user.RefreshTokenExpiryAt = DateTime.UtcNow.AddDays(Constants.RefreshTokenDurationInDays);
            user.IsRefreshTokenRevoked = false;

            return new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public static RefreshResponseDto? Refresh(RefreshRequestDto refreshRequest)
        {
            // Validate the refresh request
            if (refreshRequest == null
                || string.IsNullOrWhiteSpace(refreshRequest.Username)
                || string.IsNullOrWhiteSpace(refreshRequest.RefreshToken))
            {
                return null;
            }

            // Find the user based on the username
            var user = UsersData.Users.FirstOrDefault(
                u => u.Username == refreshRequest.Username.Trim());

            // Validate the existing refresh token
            if (user == null
                || string.IsNullOrWhiteSpace(user.RefreshTokenHash)
                || !BCrypt.Net.BCrypt.Verify(refreshRequest.RefreshToken, user.RefreshTokenHash)
                || user.RefreshTokenExpiryAt <= DateTime.UtcNow
                || user.IsRefreshTokenRevoked)
            {
                return null;
            }

            var refreshToken = GenerateRefreshToken();
            var accessToken = GenerateAccessToken(user);

            // Update the user with the new refresh token and its expiry time
            user.RefreshTokenHash = BCrypt.Net.BCrypt.HashPassword(refreshToken);
            user.RefreshTokenExpiryAt = DateTime.UtcNow.AddDays(Constants.RefreshTokenDurationInDays);
            user.IsRefreshTokenRevoked = false;

            return new RefreshResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public static void Logout(int userId)
        {
            if (userId <= 0) return;

            // Find the user based on the user ID
            var user = UsersData.Users.FirstOrDefault(
                u => u.UserId == userId);

            if (user == null) return;

            // Revoke the refresh token by setting the flag and clearing the token
            user.IsRefreshTokenRevoked = true;
            user.RefreshTokenHash = null;
            user.RefreshTokenExpiryAt = null;
        }


        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }

        private static string GenerateAccessToken(User user)
        {
            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new (ClaimTypes.Name, user.Username.Trim()),
            };

            var token = new JwtSecurityToken(
                issuer: Constants.Issuer,
                audience: Constants.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Constants.AccessTokenDurationInMinutes),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.SecretKey)), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
