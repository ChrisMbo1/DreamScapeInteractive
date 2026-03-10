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
using DreamScapeInteractive.Model;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DreamScapeInteractive.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateItemsPage : Page
    {
        public CreateItemsPage()
        {
            InitializeComponent();
        }

        private void CreateItemButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameInput.Text.Trim();
            string description = DescriptionInput.Text.Trim();
            string typeStr = (TypeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            string rarityStr = (RarityComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(typeStr) || string.IsNullOrWhiteSpace(rarityStr))
            {
                ErrorText.Text = "Name, Type, and Rarity are required.";
                ErrorText.Visibility = Visibility.Visible;
                return;
            }

            if (!int.TryParse(PowerInput.Text, out int power)) power = 0;
            if (!int.TryParse(SpeedInput.Text, out int speed)) speed = 0;
            if (!int.TryParse(DurabilityInput.Text, out int durability)) durability = 0;

            ItemType type = Enum.Parse<ItemType>(typeStr);
            Rarity rarity = Enum.Parse<Rarity>(rarityStr);

            var newItem = new Item
            {
                Name = name,
                Description = description,
                Type = type,
                Rarity = rarity,
                Power = power,
                Speed = speed,
                Durability = durability,
                Enchantments = EnchantmentsInput.Text.Trim()
            };

            using var db = new Data.AppDataContext();
            db.Items.Add(newItem);
            db.SaveChanges();

            // Reset inputs
            NameInput.Text = "";
            DescriptionInput.Text = "";
            TypeComboBox.SelectedIndex = -1;
            RarityComboBox.SelectedIndex = -1;
            PowerInput.Text = "";
            SpeedInput.Text = "";
            DurabilityInput.Text = "";
            EnchantmentsInput.Text = "";

            ErrorText.Text = "Item created successfully!";
            ErrorText.Visibility = Visibility.Visible;
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
    
