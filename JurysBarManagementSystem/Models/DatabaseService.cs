using System;
using System.Collections.Generic;
using JurysBarManagementSystem.Models;

namespace JurysBarManagementSystem.Services
{
    public static class DatabaseService
    {
        private static readonly List<SaleRecord> sales = new List<SaleRecord>();
       

        // Sales methods
        public static void AddSale(SaleRecord sale)
        {
            if (sale == null) throw new ArgumentNullException(nameof(sale));
            sales.Add(sale);
        }

        public static List<SaleRecord> GetSales()
        {
            return new List<SaleRecord>(sales);
        }

      
        
    }
}