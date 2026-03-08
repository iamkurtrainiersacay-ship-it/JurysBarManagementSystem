// Models/Receipt.cs
using System;
using System.Collections.Generic;

namespace JurysBarManagementSystem.Models
{
    public class Receipt
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? Cashier { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public List<ReceiptItem> Items { get; set; } = new List<ReceiptItem>();
    }

    public class ReceiptItem
    {
        public string? ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}