using System.Data.SQLite;

namespace JurysBarManagementSystem.Database
{
    public static class SQLiteService
    {
        private static readonly string connectionString =
            "Data Source=database/jurysbar.db;Version=3;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}