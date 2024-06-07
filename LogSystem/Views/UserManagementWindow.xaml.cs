using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LogSystem.Views
{
    /// <summary>
    /// Interaction logic for UserManagementWindow.xaml
    /// </summary>
    public partial class UserManagementWindow : Window
    {
        private readonly UserDataContext _context;

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }


        public UserManagementWindow()
        {
            InitializeComponent();
            _context = new UserDataContext();
            LoadUsers();
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            var user = new User
            {
                FullName = txtFullName.Text,
                EmployeeId = txtEmployeeId.Text,
                Username = txtUsername.Text,
                PasswordHash = HashPassword(txtPassword.Text),
                IsSuperUser = chkIsSuperUser.IsChecked.GetValueOrDefault()
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            LoadUsers();
        }

        private void LoadUsers()
        {
            dataGridUsers.ItemsSource = _context.Users.ToList();
        }

        private void userTimeLog()
        {
            var logs = _context.TimeLogs;
            dataGridLogs.ItemsSource = logs;
        }

        private void dataGridUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
