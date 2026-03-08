using System.Linq;
using System.Windows.Controls;
using JurysBarManagementSystem.Services;
using System.Collections.Generic;

namespace JurysBarManagementSystem.Views
{
    public partial class DashboardContentView : UserControl
    {
        public int ProductCount { get; set; }
        public double TotalSalesToday { get; set; }
        public int CustomersServed { get; set; }
        public List<string> RecentReceipts { get; set; }

        public DashboardContentView()
        {
            InitializeComponent();

            // Load analytics
            ProductCount = DashboardService.TotalProducts();
            TotalSalesToday = DashboardService.TotalSalesToday();

            var payments = PaymentService.GetPayments();
            CustomersServed = payments.Select(p => p.CustomerName ?? "Walk-in").Distinct().Count();

            // Load last 10 receipts
            RecentReceipts = ReceiptService.GetAllReceipts();

            TransactionsGrid.ItemsSource = payments
                .OrderByDescending(p => p.Date)
                .Take(10)
                .ToList();

            DataContext = this;
        }
    }
}