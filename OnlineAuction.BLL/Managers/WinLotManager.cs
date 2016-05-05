using System;
using System.Data.Entity.Core;
using System.Linq;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.DAL.Interfaces;
using OnlineAuction.Entities;

namespace OnlineAuction.BLL.Managers
{
    public class WinLotManager : AbstractManager<WinLot>, IWinLotManager
    {
        public WinLotManager(IRepository<WinLot> repository) : base(repository)
        {
        }

        public WinLot MakeBet(int id, int userId, decimal factor)
        {
            var winLot = Get(id);
            if (winLot != null)
            {
                if (winLot.EndTime < DateTime.Now || winLot.StarTime > DateTime.Now)
                    throw new Exception("Auction is not available now.");
                decimal price;
                if (winLot.Bets.Count > 0)
                {
                    var lastOrDefault = winLot.Bets.LastOrDefault();
                    if (lastOrDefault != null)
                        price = lastOrDefault.Value;
                    else
                        throw new ObjectNotFoundException();
                }
                else
                    price = winLot.Lot.Price;
                Bet bet = new Bet
                {
                    UserId = userId,
                    Value = price + price*factor
                };
                winLot.Bets.Add(bet);
                winLot.EndTime = winLot.EndTime.AddMinutes(1);
                Update(winLot);
                return winLot;
            }
            throw new ObjectNotFoundException($"in winLot makeBet (id = {id})");
        }
    }
}