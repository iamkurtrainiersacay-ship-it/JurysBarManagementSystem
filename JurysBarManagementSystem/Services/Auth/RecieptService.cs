using System;
using System.Collections.Generic;
using System.IO;
using JurysBarManagementSystem.Models;

namespace JurysBarManagementSystem.Services
{
    public static class ReceiptService
    {
        private static readonly string receiptsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Receipts");

        static ReceiptService()
        {
            // Ensure receipts folder exists
            if (!Directory.Exists(receiptsFolder))
                Directory.CreateDirectory(receiptsFolder);
        }

        /// <summary>
        /// Save a receipt to a text file
        /// </summary>
        public static void SaveReceipt(string customerName, List<SaleItem> items, double total, string cashier)
        {
            string fileName = $"Receipt_{DateTime.Now:yyyyMMddHHmmss}.txt";
            string filePath = Path.Combine(receiptsFolder, fileName);

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine("=================================");
                sw.WriteLine("           JURY'S BAR");
                sw.WriteLine("=================================");
                sw.WriteLine($"Cashier: {cashier}");
                sw.WriteLine($"Customer: {customerName}");
                sw.WriteLine($"Date: {DateTime.Now:yyyy-MM-dd}");
                sw.WriteLine($"Time: {DateTime.Now:HH:mm:ss}");
                sw.WriteLine("---------------------------------");

                foreach (var item in items)
                {
                    double line = item.Price * item.Quantity;
                    sw.WriteLine($"{item.ProductName} x{item.Quantity}  {line}");
                }

                sw.WriteLine("---------------------------------");
                sw.WriteLine($"TOTAL: {total}");
                sw.WriteLine("---------------------------------");
                sw.WriteLine("Thank you for visiting Jury's Bar!");
            }
        }

        /// <summary>
        /// Get all saved receipt file paths
        /// </summary>
        public static List<string> GetAllReceipts()
        {
            if (!Directory.Exists(receiptsFolder))
                return new List<string>();

            var files = Directory.GetFiles(receiptsFolder, "*.txt");
            return new List<string>(files);
        }

        /// <summary>
        /// Load a receipt text from file
        /// </summary>
        public static string LoadReceipt(string filePath)
        {
            if (!File.Exists(filePath))
                return string.Empty;

            return File.ReadAllText(filePath);
        }
    }
}