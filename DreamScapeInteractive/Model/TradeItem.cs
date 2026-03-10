using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamScapeInteractive.Model
{
    public class TradeItem
    {
        public int Id { get; set; }
        

        //fk trade
        public int TradeId { get; set; }
        public Trade Trade { get; set; }

        //fk iten

        public int ItemId { get; set; }
        public Item Item { get; set; }

        //fk user

        public int FromUserId { get; set; }
        public User User { get; set; }
    }
}
