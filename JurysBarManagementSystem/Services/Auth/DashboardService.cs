using System;
using System.Data.SQLite;
using JurysBarManagementSystem.Database;

namespace JurysBarManagementSystem.Services
{
    public static class DashboardService
    {
        public static int TotalProducts()
        {
            using var conn = SQLiteService.GetConnection();
            conn.Open();

            SQLiteCommand cmd =
                new SQLiteCommand("SELECT COUNT(*) FROM Products", conn);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static double TotalSalesToday()
        {
            using var conn = SQLiteService.GetConnection();
            conn.Open();

            string today = DateTime.Now.ToString("yyyy-MM-dd");

            SQLiteCommand cmd =
                new SQLiteCommand(
                "SELECT SUM(Total) FROM Sales WHERE Date=@d", conn);

            cmd.Parameters.AddWithValue("@d", today);

            var val = cmd.ExecuteScalar();

            if (val == DBNull.Value || val == null)
                return 0;

            return Convert.ToDouble(val);
        }
    }
}