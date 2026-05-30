using AuthCore_API.Models.DTOs.Requests;
using AuthCore_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthCore_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequestDto loginRequest)
        {
            var result = AuthService.Login(loginRequest);

            // If the result is null, it means the credentials were invalid, so we return a 401 Unauthorized response. Otherwise, we return a 200 OK response with the login result (access token and refresh token).
            return result is null ?
                Unauthorized(new { message = "Invalid credentials", code = "AUTH_001" })
                : Ok(result);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("refresh")]
        [AllowAnonymous]
        public IActionResult Refresh([FromBody] RefreshRequestDto refreshRequest)
        {
            var result = AuthService.Refresh(refreshRequest);
            return result is null ? Unauthorized("Invalid or expired refresh token") : Ok(result);
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdClaim, out var currentUserId))
                return Unauthorized();

            AuthService.Logout(currentUserId);

            // Logout is always successful,
            // even if the user was not found or the token was invalid,
            // to prevent token enumeration attacks
            return NoContent();
        }
    }
}
