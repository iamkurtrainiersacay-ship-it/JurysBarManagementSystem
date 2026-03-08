using System.Windows.Controls;
using JurysBarManagementSystem.Services;

namespace JurysBarManagementSystem.Views
{
    public partial class PaymentView : UserControl
    {
        public PaymentView()
        {
            InitializeComponent();
            LoadPayments();
        }

        private void LoadPayments()
        {
            PaymentGrid.ItemsSource = PaymentService.GetPayments();
        }
    }
}