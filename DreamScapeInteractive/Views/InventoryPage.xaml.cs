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
using DreamScapeInteractive.Model;

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
        private List<Inventory> fullInventory = new();

        private void ApplyFilter()
        {
            if (fullInventory == null || TypeFilterCombo == null || RarityFilterCombo == null)
                return;

            var filtered = fullInventory.AsEnumerable();

            if (TypeFilterCombo.SelectedItem is ComboBoxItem typeItem && typeItem.Content.ToString() != "All Types")
            {
                if (Enum.TryParse<ItemType>(typeItem.Content.ToString(), out var type))
                    filtered = filtered.Where(i => i.Item.Type == type);
            }

            if (RarityFilterCombo.SelectedItem is ComboBoxItem rarityItem && rarityItem.Content.ToString() != "All Rarity")
            {
                if (Enum.TryParse<Rarity>(rarityItem.Content.ToString(), out var rarity))
                    filtered = filtered.Where(i => i.Item.Rarity == rarity);
            }


            var filteredList = filtered.ToList();
            InventoryList.ItemsSource = filteredList;

            NoItemsText.Visibility = filteredList.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        }


        private void Filter_Changed(object sender, RoutedEventArgs e)
        {
            ApplyFilter();
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

            fullInventory = db.Inventories
                              .Include(i => i.Item)
                              .Where(i => i.UserId == Sessions.CurrentUser.Id)
                              .ToList();

            ApplyFilter();
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
