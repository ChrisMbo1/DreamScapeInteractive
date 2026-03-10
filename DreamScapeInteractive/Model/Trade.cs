using System.Collections.Generic;

namespace DreamScapeInteractive.Model
{
    public class Trade
    {
        public int Id { get; set; }
        public TradeStatus Status { get; set; } = TradeStatus.Pending;
        public int SenderId { get; set; }
        public User Sender { get; set; } = null!;

        public int ReceiverId { get; set; }
        public User Receiver { get; set; } = null!;
        public List<TradeItem> TradeItems { get; set; } = new List<TradeItem>();

        public enum TradeStatus
        {
            Pending,
            Accepted,
            Declined
        }
    }
 
}

