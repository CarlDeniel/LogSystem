using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BCrypt.Net;

namespace LogSystem.Views
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        private readonly UserDataContext _context;

        public LoginPage()
        {
            InitializeComponent();
            _context = new UserDataContext();
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameText.Text;
            var password = PasswordText.Password;

            var user = _context.Users.SingleOrDefault(u => u.Username == username);

            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                if (user.IsSuperUser)
                {
                    // Open User Management Window
                    var userManagementWindow = new UserManagementWindow();
                    userManagementWindow.Show();
                }
                else
                {
                    // Open main application window
                    var mainWindow = new MainWindow(user);
                    mainWindow.Show();
                }

                // Close the parent window of the UserControl
                Window.GetWindow(this).Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void UsernameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Handle text changed event if needed
        }
    }
}
