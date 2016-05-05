using OnlineAuction.Entities;

namespace OnlineAuction.BLL.Interfaces
{
    public interface IWinLotManager : IManager<WinLot>
    {
        WinLot MakeBet(int id, int userId, decimal factor);
    }
}
