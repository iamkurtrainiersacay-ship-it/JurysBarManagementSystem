using JurysBarManagementSystem.Database;
using JurysBarManagementSystem.Services;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace JurysBarManagementSystem.Views
{
    public partial class RegisterUserView : UserControl
    {
        public RegisterUserView()
        {
            InitializeComponent();
        }

        #region Placeholder Logic
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text == "Username")
            {
                (sender as TextBox).Text = "";
                (sender as TextBox).Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace((sender as TextBox).Text))
            {
                (sender as TextBox).Text = "Username";
                (sender as TextBox).Foreground = Brushes.Gray;
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordBox.Password))
                PasswordPlaceholder.Visibility = Visibility.Visible;
        }
        #endregion

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Password;
            string role = (RoleBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (username == "Username" || string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string hashedPassword = PasswordHelper.HashPassword(password);

            try
            {
                using var conn = SQLiteService.GetConnection();
                conn.Open();

                // Check duplicate username
                string checkSql = "SELECT COUNT(*) FROM Users WHERE Username=@username";
                using var checkCmd = new SQLiteCommand(checkSql, conn);
                checkCmd.Parameters.AddWithValue("@username", username);
                long count = (long)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Username already exists. Choose another.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Insert new user
                string sql = "INSERT INTO Users (Username, Password, Role) VALUES (@username, @password, @role)";
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", hashedPassword);
                cmd.Parameters.AddWithValue("@role", role);
                cmd.ExecuteNonQuery();
            }
            catch (System.Data.SQLite.SQLiteException ex)
            {
                MessageBox.Show("Error saving user: " + ex.Message);
                return;
            }

            MessageBox.Show($"User '{username}' registered as {role} successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Close registration window
            Window.GetWindow(this)?.Close();

            // Open login window automatically
            LoginView loginWindow = new LoginView();
            loginWindow.ShowDialog();
        }
    }
}