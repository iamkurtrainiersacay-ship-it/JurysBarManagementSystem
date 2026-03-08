using JurysBarManagementSystem.Models;
using JurysBarManagementSystem.Models.User;
using JurysBarManagementSystem.Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace JurysBarManagementSystem.Views
{
    public partial class SalesPage : Page
    {
        private List<SaleItem> order = new();

        public SalesPage()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            ProductsGrid.ItemsSource = ProductService.GetProducts();
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem is not Product p)
            {
                MessageBox.Show("Select a product.");
                return;
            }

            if (!int.TryParse(QuantityBox.Text, out int qty) || qty <= 0)
            {
                MessageBox.Show("Invalid quantity.");
                return;
            }

            order.Add(new SaleItem
            {
                ProductName = p.Name,
                Quantity = qty,
                Price = p.Price
            });

            OrderGrid.ItemsSource = null;
            OrderGrid.ItemsSource = order;
        }

        private void ProcessOrder_Click(object sender, RoutedEventArgs e)
        {
            string customerName = CustomerBox.Text;
            if (string.IsNullOrWhiteSpace(customerName) || customerName == "Customer Name")
            {
                MessageBox.Show("Enter a customer name.");
                return;
            }

            if (order.Count == 0)
            {
                MessageBox.Show("No items in order.");
                return;
            }

            // Process sale
            SalesService.ProcessSale(order, customerName);

            // Generate receipt
            double total = 0;

            foreach (var item in order)
            {
                total += item.Price * item.Quantity;
            }

            string cashier = AuthService.CurrentUser?.Username ?? "Unknown";

            ReceiptService.SaveReceipt(customerName, order, total, cashier);

            ReceiptBox.Text = "Receipt saved successfully.";

            MessageBox.Show("Sale completed! Receipt generated.");

            // Clear order
            order.Clear();
            OrderGrid.ItemsSource = null;
            CustomerBox.Text = "Customer Name";
            CustomerBox.Foreground = Brushes.Gray;

            LoadProducts();
        }

        #region Customer Placeholder
        private void CustomerBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CustomerBox.Text == "Customer Name")
            {
                CustomerBox.Text = "";
                CustomerBox.Foreground = Brushes.Black;
            }
        }

        private void CustomerBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CustomerBox.Text))
            {
                CustomerBox.Text = "Customer Name";
                CustomerBox.Foreground = Brushes.Gray;
            }
        }
        #endregion
    }
}