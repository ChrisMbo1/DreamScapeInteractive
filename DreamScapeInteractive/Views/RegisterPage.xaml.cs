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
using BCrypt;
using DreamScapeInteractive.Model;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DreamScapeInteractive.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameInput.Text;
            string password = PasswordInput.Password;
            string confirmPassword = ConfirmPasswordInput.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                ErrorText.Text = "Please fill in all fields";
                ErrorText.Visibility = Visibility.Visible;
                return;
            }

            if (password != confirmPassword)
            {
                ErrorText.Text = "Passwords do not match";
                ErrorText.Visibility = Visibility.Visible;
                return;
            }

            using var db = new Data.AppDataContext();

            if (db.Users.Any(u => u.Username == username))
            {
                ErrorText.Text = "Username already exists";
                ErrorText.Visibility = Visibility.Visible;
                return;
            }

            var userRole = db.Roles.FirstOrDefault(r => r.Roles == "User");
            if (userRole == null) throw new Exception("User role not found");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var newUser = new User
            {
                Username = username,
                PasswordHash = hashedPassword,
                RoleId = userRole.Id
            };

            db.Users.Add(newUser);
            db.SaveChanges();

            Frame.Navigate(typeof(LoginPage));
        }

        private void GoToLoginPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}

