using System.Windows.Controls;
using JurysBarManagementSystem.Services;

namespace JurysBarManagementSystem.Views
{
    public partial class SalesView : UserControl
    {
        public SalesView()
        {
            InitializeComponent();
            LoadSales();
        }

        private void LoadSales()
        {
            SalesGrid.ItemsSource = SalesService.GetSales();
        }
    }
}