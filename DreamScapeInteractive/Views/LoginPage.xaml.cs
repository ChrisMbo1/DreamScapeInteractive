using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using DreamScapeInteractive.Model;
using Microsoft.EntityFrameworkCore;
using DreamScapeInteractive.Session;
using BCrypt;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DreamScapeInteractive.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameInput.Text;
            string password = PasswordInput.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ErrorText.Text = "Please enter username and password";
                ErrorText.Visibility = Visibility.Visible;
                return;
            }

            using var db = new Data.AppDataContext();

            var user = db.Users
                         .Include(u => u.Role)
                         .FirstOrDefault(u => u.Username == username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                ErrorText.Text = "Invalid username or password";
                ErrorText.Visibility = Visibility.Visible;
                return;
            }

            Sessions.CurrentUser = user;

            if (user.Role != null && user.Role.Roles == "Admin")
            {
                Frame.Navigate(typeof(AdminPage));
            }
            else
            {
                Frame.Navigate(typeof(HomePage));
            }
        }


        private void GoToRegister_Click(object sender, RoutedEventArgs e)
        {

            Frame.Navigate(typeof(RegisterPage));
        }

        

    }
}
