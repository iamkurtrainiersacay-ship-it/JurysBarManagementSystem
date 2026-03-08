using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using JurysBarManagementSystem.Models;
using JurysBarManagementSystem.Services;

namespace JurysBarManagementSystem.Views
{
    public partial class ProductView : UserControl
    {
        public ProductView()
        {
            InitializeComponent();
            LoadProducts();

            // Role-based UI
            if (AuthService.CurrentUser.Role == "Cashier")
            {
                NameBox.Visibility = Visibility.Collapsed;
                PriceBox.Visibility = Visibility.Collapsed;
                StockBox.Visibility = Visibility.Collapsed;
                AddProductButton.Visibility = Visibility.Collapsed;
            }
        }

        private void LoadProducts()
        {
            ProductGrid.ItemsSource = ProductService.GetProducts();
        }

        #region Placeholder Logic
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (tb == NameBox && tb.Text == "Product Name") tb.Text = "";
                if (tb == PriceBox && tb.Text == "Price") tb.Text = "";
                if (tb == StockBox && tb.Text == "Stock") tb.Text = "";

                tb.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (tb == NameBox && string.IsNullOrWhiteSpace(tb.Text)) tb.Text = "Product Name";
                if (tb == PriceBox && string.IsNullOrWhiteSpace(tb.Text)) tb.Text = "Price";
                if (tb == StockBox && string.IsNullOrWhiteSpace(tb.Text)) tb.Text = "Stock";

                tb.Foreground = Brushes.Gray;
            }
        }
        #endregion

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            string name = NameBox.Text.Trim();
            bool priceOk = double.TryParse(PriceBox.Text.Trim(), out double price);
            bool stockOk = int.TryParse(StockBox.Text.Trim(), out int stock);

            if (string.IsNullOrEmpty(name) || name == "Product Name" || !priceOk || !stockOk)
            {
                MessageBox.Show("Please enter valid product details.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ProductService.AddProduct(new Product
            {
                Name = name,
                Price = price,
                Stock = stock
            });

            MessageBox.Show($"Product '{name}' added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            NameBox.Text = "Product Name";
            PriceBox.Text = "Price";
            StockBox.Text = "Stock";

            NameBox.Foreground = PriceBox.Foreground = StockBox.Foreground = Brushes.Gray;

            LoadProducts();
        }
    }
}