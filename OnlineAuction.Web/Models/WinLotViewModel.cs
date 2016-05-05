using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using OnlineAuction.Entities;

namespace OnlineAuction.Models
{
    public class WinLotViewModel
    {
        public int WinLotId { get; set; }
        public int LotId { get; set; }
        public Lot Lot { get; set; }
        public Bet LastBet { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy hh:mm}")]
        public DateTime StartTime { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy hh:mm}")]
        public DateTime EndTime { get; set; }

        public WinLotViewModel() { }

        public WinLotViewModel(WinLot model)
        {
            WinLotId = model.WinLotId;
            LastBet = model.Bets.LastOrDefault();
            LotId = model.LotId;
            Lot = model.Lot;
            StartTime = model.StarTime;
            EndTime = model.EndTime;
        }

        public void SetModel(WinLot model)
        {
            model.EndTime = EndTime;
            model.StarTime = StartTime;
            model.LotId = Lot.LotId;
        }
    }
}