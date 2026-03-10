using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamScapeInteractive.Model
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        // Foreign key to User
        public int UserId { get; set; }
        public User User { get; set; }

        public int TradeId { get; set; }
        public Trade Trade { get; set; }
    }
}
