using System;
using System.Collections.Generic;

namespace OnlineAuction.Entities
{
    public class WinLot
    {
        public int WinLotId { get; set; }
        public int LotId { get; set; }
        public virtual Lot Lot { get; set; }
        public virtual List<Bet> Bets { get; set; }
        public DateTime StarTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
