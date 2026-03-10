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
    public sealed partial class EditItemsPage : Page
    {
        private Item selectedItem;

        public EditItemsPage()
        {
            this.InitializeComponent();
            LoadItems();
        }

        private void LoadItems()
        {
            using var db = new Data.AppDataContext();
            ItemsListView.ItemsSource = db.Items.ToList();
        }

        private void ItemsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = ItemsListView.SelectedItem as Item;
            if (selectedItem != null)
            {
                NameInput.Text = selectedItem.Name;
                DescriptionInput.Text = selectedItem.Description;
                TypeComboBox.SelectedIndex = (int)selectedItem.Type;
                RarityComboBox.SelectedIndex = (int)selectedItem.Rarity;
                PowerInput.Text = selectedItem.Power.ToString();
                SpeedInput.Text = selectedItem.Speed.ToString();
                DurabilityInput.Text = selectedItem.Durability.ToString();
                EnchantmentsInput.Text = selectedItem.Enchantments;
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (selectedItem == null) return;

            using var db = new Data.AppDataContext();
            var item = db.Items.FirstOrDefault(i => i.Id == selectedItem.Id);
            if (item != null)
            {
                item.Name = NameInput.Text;
                item.Description = DescriptionInput.Text;
                item.Type = (ItemType)TypeComboBox.SelectedIndex;
                item.Rarity = (Rarity)RarityComboBox.SelectedIndex;
                item.Power = int.TryParse(PowerInput.Text, out int p) ? p : 0;
                item.Speed = int.TryParse(SpeedInput.Text, out int s) ? s : 0;
                item.Durability = int.TryParse(DurabilityInput.Text, out int d) ? d : 0;
                item.Enchantments = EnchantmentsInput.Text;

                db.SaveChanges();
            }

            LoadItems();
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (selectedItem == null) return;

            using var db = new Data.AppDataContext();
            var item = db.Items.FirstOrDefault(i => i.Id == selectedItem.Id);
            if (item != null)
            {
                db.Items.Remove(item);
                db.SaveChanges();
            }

            selectedItem = null;
            ClearInputs();
            LoadItems();
        }

        private void ClearInputs()
        {
            NameInput.Text = "";
            DescriptionInput.Text = "";
            TypeComboBox.SelectedIndex = -1;
            RarityComboBox.SelectedIndex = -1;
            PowerInput.Text = "";
            SpeedInput.Text = "";
            DurabilityInput.Text = "";
            EnchantmentsInput.Text = "";
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
