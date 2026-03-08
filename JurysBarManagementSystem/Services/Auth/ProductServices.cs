using JurysBarManagementSystem.Database;
using JurysBarManagementSystem.Models;
using JurysBarManagementSystem.Models.User;
using System.Data.SQLite;

namespace JurysBarManagementSystem.Services
{
    public static class ProductService
    {
        public static List<Product> GetProducts()
        {
            List<Product> products = new();

            using var conn = SQLiteService.GetConnection();
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Products", conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Price = Convert.ToDouble(reader["Price"]),
                    Stock = Convert.ToInt32(reader["Stock"])
                });
            }

            return products;
        }

        public static void AddProduct(Product p)
        {
            using var conn = SQLiteService.GetConnection();
            conn.Open();

            string sql = "INSERT INTO Products(Name,Price,Stock) VALUES(@n,@p,@s)";

            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.AddWithValue("@n", p.Name);
            cmd.Parameters.AddWithValue("@p", p.Price);
            cmd.Parameters.AddWithValue("@s", p.Stock);

            cmd.ExecuteNonQuery();
        }

        public static void UpdateStock(string productName, int quantity)
        {
            using var conn = SQLiteService.GetConnection();
            conn.Open();

            string sql = "UPDATE Products SET Stock = Stock - @q WHERE Name=@n";

            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.AddWithValue("@q", quantity);
            cmd.Parameters.AddWithValue("@n", productName);

            cmd.ExecuteNonQuery();
        }
    }
}