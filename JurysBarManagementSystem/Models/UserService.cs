using System.Collections.Generic;
using System.Data.SQLite;
using JurysBarManagementSystem.Database;
using JurysBarManagementSystem.Models;

namespace JurysBarManagementSystem.Services
{
    public static class UserService
    {
        // ===============================
        // GET USERS
        // ===============================

        public static List<Users> GetUsers()
        {
            List<Users> users = new();

            using var conn = SQLiteService.GetConnection();
            conn.Open();

            using var cmd = new SQLiteCommand("SELECT * FROM Users", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                users.Add(new Users
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Password = reader.GetString(2),
                    Role = reader.GetString(3)
                });
            }

            return users;
        }

        // ===============================
        // ADD USER
        // ===============================

        public static void AddUser(Users user)
        {
            using var conn = SQLiteService.GetConnection();
            conn.Open();

            string sql =
                "INSERT INTO Users(Username,Password,Role) VALUES(@u,@p,@r)";

            using var cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.AddWithValue("@u", user.Username);
            cmd.Parameters.AddWithValue("@p", user.Password);
            cmd.Parameters.AddWithValue("@r", user.Role);

            cmd.ExecuteNonQuery();
        }

        // ===============================
        // DELETE USER
        // ===============================

        public static void DeleteUser(int id)
        {
            using var conn = SQLiteService.GetConnection();
            conn.Open();

            using var cmd = new SQLiteCommand(
                "DELETE FROM Users WHERE Id=@id", conn);

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
    }
}