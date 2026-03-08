using System.Windows;
using JurysBarManagementSystem.Services;

namespace JurysBarManagementSystem.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Password;

            var user = AuthService.Authenticate(username, password);

            if (user == null)
            {
                MessageBox.Show("Invalid credentials!", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DashboardView dashboard = new DashboardView(user);
            dashboard.Show();
            this.Close();
        }
    }
}