using System.Windows.Controls;
using System.Windows;
using JurysBarManagementSystem.Services;
using System.IO;

namespace JurysBarManagementSystem.Views
{
    public partial class ReceiptsView : UserControl
    {
        public ReceiptsView()
        {
            InitializeComponent();
            LoadReceipts();
        }

        private void LoadReceipts()
        {
            ReceiptsStack.Children.Clear();
            var receipts = ReceiptService.GetAllReceipts();

            foreach (var file in receipts)
            {
                string text = ReceiptService.LoadReceipt(file);

                // Create a border box for each receipt
                Border border = new Border
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = System.Windows.Media.Brushes.Gray,
                    Margin = new Thickness(0, 0, 0, 10),
                    Padding = new Thickness(10),
                    CornerRadius = new CornerRadius(5)
                };

                StackPanel sp = new StackPanel();

                TextBlock tb = new TextBlock
                {
                    Text = text,
                    FontFamily = new System.Windows.Media.FontFamily("Consolas"),
                    FontSize = 14
                };

                Button downloadBtn = new Button
                {
                    Content = "Download",
                    Width = 100,
                    Margin = new Thickness(0, 5, 0, 0),
                    Tag = file
                };
                downloadBtn.Click += DownloadBtn_Click;

                sp.Children.Add(tb);
                sp.Children.Add(downloadBtn);

                border.Child = sp;
                ReceiptsStack.Children.Add(border);
            }
        }

        private void DownloadBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string path && File.Exists(path))
            {
                // Open SaveFileDialog to choose download location
                Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog
                {
                    FileName = Path.GetFileName(path),
                    Filter = "Text files (*.txt)|*.txt"
                };

                if (sfd.ShowDialog() == true)
                {
                    File.Copy(path, sfd.FileName, true);
                    MessageBox.Show($"Receipt saved as {sfd.FileName}", "Downloaded", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}