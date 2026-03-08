using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using JurysBarManagementSystem.Models.User;
using JurysBarManagementSystem.Services;

namespace JurysBarManagementSystem.Views
{
    public partial class PayrollView : UserControl
    {
        public PayrollView()
        {
            InitializeComponent();
            LoadPayroll();
        }

        // ============================
        // Load Payroll Records
        // ============================

        private void LoadPayroll()
        {
            PayrollGrid.ItemsSource = PayrollService.GetPayrolls();
        }

        // ============================
        // Create Payroll
        // ============================

        private void CreatePayroll_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(SalaryBox.Text, out double salary))
            {
                MessageBox.Show("Invalid salary rate");
                return;
            }

            if (!double.TryParse(HoursBox.Text, out double hours))
            {
                MessageBox.Show("Invalid hours");
                return;
            }

            double bonus = 0;
            double.TryParse(BonusBox.Text, out bonus);

            PayrollService.CreatePayroll(
                EmployeeBox.Text,
                RoleBox.Text,
                salary,
                hours,
                bonus
            );

            MessageBox.Show("Payroll created successfully!");

            LoadPayroll();
        }

        // ============================
        // Placeholder Logic
        // ============================

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                tb.Foreground = Brushes.Black;

                if (tb.Text.Contains("Name") ||
                    tb.Text.Contains("Role") ||
                    tb.Text.Contains("Salary") ||
                    tb.Text.Contains("Hours") ||
                    tb.Text.Contains("Bonus"))
                {
                    tb.Text = "";
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Foreground = Brushes.Gray;
            }
        }
    }
}