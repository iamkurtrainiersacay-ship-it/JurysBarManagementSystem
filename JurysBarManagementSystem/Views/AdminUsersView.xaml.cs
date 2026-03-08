using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using JurysBarManagementSystem.Models;
using JurysBarManagementSystem.Services;

namespace JurysBarManagementSystem.Views
{
    public partial class AdminUsersView : UserControl
    {
        public AdminUsersView()
        {
            InitializeComponent();

            if (!PermissionService.CanManageAccounts(
                AuthService.CurrentUser.Role == "Admin" ? UserRole.Admin :
                AuthService.CurrentUser.Role == "Manager" ? UserRole.Manager :
                UserRole.Staff))
            {
                MessageBox.Show("You are not allowed to manage accounts.");
                return;
            }

            LoadUsers();
        }

        private void LoadUsers()
        {
            UsersGrid.ItemsSource = UserService.GetUsers();
        }

        // ================================
        // Add User
        // ================================

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;

            if (string.IsNullOrWhiteSpace(username) ||
                PasswordBox.Password.Length < 3 ||
                RoleBox.SelectedItem == null)
            {
                MessageBox.Show("Fill all fields.");
                return;
            }

            string role = (RoleBox.SelectedItem as ComboBoxItem).Content.ToString();

            UserService.AddUser(new Users
            {
                Username = username,
                Password = PasswordHelper.HashPassword(PasswordBox.Password),
                Role = role
            });

            LoadUsers();
        }

        // ================================
        // Update User
        // ================================

        private void UpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedItem is not Users user)
            {
                MessageBox.Show("Select user to update.");
                return;
            }

            string role = (RoleBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            user.Username = UsernameBox.Text;

            if (!string.IsNullOrEmpty(PasswordBox.Password))
            {
                user.Password = PasswordHelper.HashPassword(PasswordBox.Password);
            }

            if (role != null)
                user.Role = role;

            UserService.DeleteUser(user.Id);
            UserService.AddUser(user);

            LoadUsers();
        }

        // ================================
        // Delete User
        // ================================

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedItem is Users user)
            {
                UserService.DeleteUser(user.Id);
                LoadUsers();
            }
        }

        // ================================
        // Placeholder Logic
        // ================================

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UsernameBox.Text == "Username")
            {
                UsernameBox.Text = "";
                UsernameBox.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameBox.Text))
            {
                UsernameBox.Text = "Username";
                UsernameBox.Foreground = Brushes.Gray;
            }
        }
    }
}