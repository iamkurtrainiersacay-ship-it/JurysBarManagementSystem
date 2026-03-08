using System;

namespace JurysBarManagementSystem.Models
{
    public class SaleRecord
    {
        public string? CustomerName { get; set; }
        public string? CashierName { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        public DateTime Date { get; set; }
    }
}