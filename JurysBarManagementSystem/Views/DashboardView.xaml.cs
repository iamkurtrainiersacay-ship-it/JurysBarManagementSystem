using JurysBarManagementSystem.Services;
using System.Windows;

namespace JurysBarManagementSystem.Views
{
    public partial class DashboardView : Window
    {
        private string CurrentRole => AuthService.CurrentUser?.Role ?? "";

        public DashboardView(Models.Users user)
        {
            InitializeComponent();

            MainFrame.Navigate(new DashboardContentView());

            ApplyRoleSecurity();
        }

        // ===============================
        // Role-Based Sidebar Security
        // ===============================

        private void ApplyRoleSecurity()
        {
            if (AuthService.CurrentUser == null)
                return;

            string role = CurrentRole;

            bool canManageAccounts =
                role == "Admin" ||
                role == "Manager" ||
                role == "SuperAdmin";

            UsersButton.Visibility =
                canManageAccounts ? Visibility.Visible : Visibility.Collapsed;

            bool canAccessPayroll =
                role == "Admin" ||
                role == "Manager" ||
                role == "SuperAdmin";

            PayrollButton.Visibility =
                canAccessPayroll ? Visibility.Visible : Visibility.Collapsed;

            // Cashier restrictions
            if (role == "Cashier")
            {
                ProductsButton.Visibility = Visibility.Collapsed;
                PayrollButton.Visibility = Visibility.Collapsed;
                PaymentsButton.Visibility = Visibility.Collapsed;
                UsersButton.Visibility = Visibility.Collapsed;
            }
        }

        // ===============================
        // Navigation Methods
        // ===============================

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DashboardContentView());
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProductView());
        }

        private void Sales_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SalesPage());
        }

        private void Payroll_Click(object sender, RoutedEventArgs e)
        {
            if (IsAuthorized())
                MainFrame.Navigate(new PayrollView());
        }

        private void Payments_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PaymentView());
        }

        private void Receipts_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ReceiptsView());
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            if (IsAuthorized())
                MainFrame.Navigate(new AdminUsersView());
        }

        private void Tnc_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TncView());
        }

        private bool IsAuthorized()
        {
            string role = CurrentRole;

            return role == "Admin" ||
                   role == "Manager" ||
                   role == "SuperAdmin";
        }

        // ===============================
        // Logout
        // ===============================

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            AuthService.Logout();

            LoginView login = new LoginView();
            login.Show();

            Close();
        }
    }
}