using System.Data.SQLite;
using System.IO;

namespace JurysBarManagementSystem.Database
{
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            if (!Directory.Exists("database"))
                Directory.CreateDirectory("database");

            string dbPath = "database/jurysbar.db";

            if (!File.Exists(dbPath))
                SQLiteConnection.CreateFile(dbPath);

            using var conn = SQLiteService.GetConnection();
            conn.Open();

            string sql = @"

CREATE TABLE IF NOT EXISTS Users(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Username TEXT UNIQUE,
    Password TEXT,
    Role TEXT
);

CREATE TABLE IF NOT EXISTS Products(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT,
    Price REAL,
    Stock INTEGER
);

CREATE TABLE IF NOT EXISTS Customers(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT,
    Contact TEXT,
    LoyaltyPoints INTEGER DEFAULT 0
);

CREATE TABLE IF NOT EXISTS Sales(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    CustomerName TEXT,
    Cashier TEXT,
    Total REAL,
    Date TEXT,
    Time TEXT
);

CREATE TABLE IF NOT EXISTS SaleItems(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    SaleId INTEGER,
    ProductName TEXT,
    Quantity INTEGER,
    Price REAL
);

CREATE TABLE IF NOT EXISTS Payments(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    CustomerName TEXT,
    Amount REAL,
    Date TEXT
);

CREATE TABLE IF NOT EXISTS Payroll(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    EmployeeName TEXT,
    Role TEXT,
    Salary REAL,
    HoursWorked REAL,
    Bonus REAL,
    Deductions REAL,
    NetPay REAL,
    Date TEXT
);

INSERT OR IGNORE INTO Users(Username,Password,Role)
VALUES('admin','admin','SuperAdmin');

";

            using var cmd = new SQLiteCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }
    }
}