using System.Windows;

namespace JurysBarManagementSystem.Views
{
    public partial class WelcomeView : Window
    {
        public WelcomeView()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterUserView registerControl = new RegisterUserView();
            Window registerWindow = new Window
            {
                Title = "Register New User",
                Content = registerControl,
                Width = 350,
                Height = 400,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Owner = this
            };
            registerWindow.ShowDialog();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginWindow = new LoginView
            {
                Owner = this
            };
            loginWindow.ShowDialog();
        }
    }
}