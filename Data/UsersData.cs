using AuthCore_API.Models;

namespace AuthCore_API.Data
{
    public static class UsersData
    {
        public static List<User> Users =
        [
            new User
            {
                UserId = 1,
                Username = "mohamedragheb",
                Email = "mohamed@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234"),
                Role = "Admin",
            },

            new User
            {
                UserId = 2,
                Username = "ahmedali",
                Email = "ahmed@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234"),
                Role = "User"
            },

            new User
            {
                UserId = 3,
                Username = "sarahmohamed",
                Email = "sarah@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234"),
                Role = "Manager"
            }
        ];
    }
}