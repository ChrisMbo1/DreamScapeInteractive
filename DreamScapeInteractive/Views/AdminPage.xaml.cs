using DreamScapeInteractive.Model;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DreamScapeInteractive.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminPage : Page
    {
        public AdminPage()
        {
            this.InitializeComponent();
            LoadUsers();
            LoadItems();
        }

        private void LoadUsers()
        {
            using var db = new Data.AppDataContext();
            var users = db.Users.ToList();
            UsersList.ItemsSource = users;
        }

        private void LoadItems()
        {
            using var db = new Data.AppDataContext();
            var items = db.Items.ToList();
            ItemsListView.ItemsSource = items;
        }

        private void GiveSelectedItems_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = UsersList.SelectedItem as User;
            if (selectedUser == null) return;

            if (!int.TryParse(QuantityInput.Text, out int quantity) || quantity <= 0)
            {
                return;
            }

            using var db = new Data.AppDataContext();

            // Get the checked items
            foreach (var item in ItemsListView.SelectedItems.Cast<Item>())
            {
                var inventory = db.Inventories.FirstOrDefault(i => i.UserId == selectedUser.Id && i.ItemId == item.Id);
                if (inventory != null)
                {
                    inventory.Quantity += quantity;
                }
                else
                {
                    db.Inventories.Add(new Inventory
                    {
                        UserId = selectedUser.Id,
                        ItemId = item.Id,
                        Quantity = quantity
                    });
                }
            }

            db.SaveChanges();

            ItemsListView.SelectedItems.Clear();
            QuantityInput.Text = string.Empty;
        }

        private void CreateItem_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreateItemsPage));
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditItemsPage));
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