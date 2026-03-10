using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DreamScapeInteractive.Model
{
    public enum Rarity { Common = 0, Uncommon = 1, Rare = 2, Epic = 3, Legendary = 4 }
    public enum ItemType
    { Weapon = 0,Armor = 1, Consumable = 2,Accessory = 3, Potion = 4 }


    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ItemType Type { get; set; }

        public Rarity Rarity { get; set; }

        public int Power { get; set; }

        public int Speed { get; set; }

        public int Durability { get; set; }

        public string Enchantments { get; set; } = string.Empty;

     /*   INSERT INTO `roles` (`Name`)
VALUES 
    ('Admin'),
    ('User');*/
    }
}
