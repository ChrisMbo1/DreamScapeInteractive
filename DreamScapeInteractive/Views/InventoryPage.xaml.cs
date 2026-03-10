using DreamScapeInteractive.Session;
using Microsoft.EntityFrameworkCore;
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
    public sealed partial class InventoryPage : Page
    {
        public InventoryPage()
        {
            InitializeComponent();
            LoadInventory();
        }


        private void LoadInventory()
        {
            if (Sessions.CurrentUser == null)
            {
                NoItemsText.Text = "You must be logged in to see your inventory.";
                NoItemsText.Visibility = Visibility.Visible;
                return;
            }

            using var db = new Data.AppDataContext();

            var inventory = db.Inventories
                              .Include(i => i.Item)
                              .Where(i => i.UserId == Sessions.CurrentUser.Id)
                              .ToList();

            if (inventory.Count == 0)
            {
                NoItemsText.Visibility = Visibility.Visible;
            }
            else
            {
                InventoryList.ItemsSource = inventory;
            }
        }
    }
        
}
