using System;
using System.Collections.Generic;
using JurysBarManagementSystem.Models;
using JurysBarManagementSystem.Models.User;

namespace JurysBarManagementSystem.Services
{
    public static class SalesService
    {
        

      
        public static void ProcessSale(List<SaleItem> order, string customerName)
        {
            if (order == null || order.Count == 0)
                throw new ArgumentException("Order cannot be empty.");

            
            double total = 0;
            foreach (var item in order)
            {
                total += item.Price * item.Quantity;
            }

           
            string cashier = AuthService.CurrentUser?.Username ?? "Unknown";

            
            ReceiptService.SaveReceipt(customerName, order, total, cashier);

            foreach (var item in order)
            {
                var record = new SaleRecord
                {
                    CustomerName = customerName,
                    CashierName = cashier,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Total = item.Price * item.Quantity,
                    Date = DateTime.Now
                };

                Services.DatabaseService.AddSale(record); 
            }
        }

        
        public static List<SaleRecord> GetSales()
        {
            return DatabaseService.GetSales();
        }
    }
}