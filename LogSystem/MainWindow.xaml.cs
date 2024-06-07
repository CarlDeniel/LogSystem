using System;
using System.Windows;
using System.Windows.Threading;

namespace LogSystem.Views
{
    public partial class MainWindow : Window
    {
        private readonly User _currentUser;
        private readonly UserDataContext _context;
        private TimeLog _currentLog;
        private DispatcherTimer _timer;

        public MainWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _context = new UserDataContext();
            LoadLogs();

            // Start the timer for the timer display
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();

            // Update the clock display
            UpdateClock();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the timer display
            // For example, you can calculate elapsed time since login
            // and display it in the timerDisplay TextBlock
            // timerDisplay.Text = elapsedTime.ToString();
        }

        private void UpdateClock()
        {
            // Update the clock display
            clockDisplay.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void BtnLogIn_Click(object sender, RoutedEventArgs e)
        {
            _currentLog = new TimeLog
            {
                UserId = _currentUser.Id,
                LoginTime = DateTime.Now
            };
            _context.TimeLogs.Add(_currentLog);
            _context.SaveChanges();

            btnLogIn.Visibility = Visibility.Hidden;
            btnLogOut.Visibility = Visibility.Visible;
        }

        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            _currentLog.LogoutTime = DateTime.Now;
            _currentLog.RenderedTime = _currentLog.LogoutTime - _currentLog.LoginTime;
            _context.SaveChanges();

            btnLogIn.Visibility = Visibility.Visible;
            btnLogOut.Visibility = Visibility.Hidden;
            LoadLogs();
        }

        private void LoadLogs()
        {
            var logs = _context.TimeLogs.Where(t => t.UserId == _currentUser.Id).ToList();
            dataGridLogs.ItemsSource = logs;
        }
    }
}
