using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamScapeInteractive.Model
{
    public class Trade
    {
        public int Id { get; set; }

        public string Status { get; set; }

        // Sender
       public int SenderId { get; set; }
        public User Sender { get; set; }

        // Receiver
        public int ReceiverId { get; set; }
        public User Receiver { get; set; }
    }
}
