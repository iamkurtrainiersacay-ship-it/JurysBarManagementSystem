using System;
using System.Data.SQLite;
using JurysBarManagementSystem.Database;
using JurysBarManagementSystem.Models;

namespace JurysBarManagementSystem.Services
{
    public static class AuthService
    {
        public static Users? CurrentUser { get; private set; }

        public static Users? Authenticate(string username, string password)
        {
            using var conn = SQLiteService.GetConnection();
            conn.Open();

            string sql = "SELECT * FROM Users WHERE Username=@u";
            using var cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@u", username);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string storedHash = reader["Password"].ToString() ?? "";

                if (!PasswordHelper.VerifyPassword(password, storedHash))
                    return null;

                CurrentUser = new Users
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Username = reader["Username"].ToString(),
                    Password = storedHash,
                    Role = reader["Role"].ToString()
                };

                return CurrentUser;
            }

            return null;
        }
        public static void SetCurrentUser(Users user)
        {
            CurrentUser = user;
        }
        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}