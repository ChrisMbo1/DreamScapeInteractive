using Microsoft.UI.Xaml.Automation.Peers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamScapeInteractive.Model
{
    public class Inventory
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        // Fk user
        public int UserId { get; set; }
        public User User { get; set; } = null!; 

        // Fk Item
        public int ItemId { get; set; }
        public Item Item { get; set; } = null!;

    }
}
