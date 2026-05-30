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
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234"),
            },

            new User
            {
                UserId = 2,
                Username = "ahmedali",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234"),
            },

            new User
            {
                UserId = 3,
                Username = "sarahmohamed",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234"),
            }
        ];
    }
}