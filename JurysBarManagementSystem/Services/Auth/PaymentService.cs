using System;
using System.Collections.Generic;
using System.Data.SQLite;
using JurysBarManagementSystem.Database;
using JurysBarManagementSystem.Models.User;

namespace JurysBarManagementSystem.Services
{
    public static class PaymentService
    {
        public static List<Payment> GetPayments()
        {
            List<Payment> payments = new();

            using var conn = SQLiteService.GetConnection();
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Payments", conn);

            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                payments.Add(new Payment
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    CustomerName = reader["CustomerName"].ToString(),
                    Amount = Convert.ToDouble(reader["Amount"]),
                    Date = reader["Date"].ToString()
                });
            }

            return payments;
        }

        public static void AddPayment(Payment payment)
        {
            using var conn = SQLiteService.GetConnection();
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand(
                "INSERT INTO Payments(CustomerName,Amount,Date) VALUES(@c,@a,@d)",
                conn);

            cmd.Parameters.AddWithValue("@c", payment.CustomerName);
            cmd.Parameters.AddWithValue("@a", payment.Amount);
            cmd.Parameters.AddWithValue("@d", payment.Date);

            cmd.ExecuteNonQuery();
        }
    }
}