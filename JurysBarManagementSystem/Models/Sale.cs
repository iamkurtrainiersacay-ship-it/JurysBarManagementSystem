using System.Collections.Generic;

namespace JurysBarManagementSystem.Models.User
{
    public class Sale
    {
        public int Id { get; set; }

        public string? Cashier { get; set; }

        public double Total { get; set; }

        public string? Date { get; set; }

        public string? Time { get; set; }

        public List<SaleItem> Items { get; set; }
    }
}