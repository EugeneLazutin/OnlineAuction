using OnlineAuction.Entities;

namespace OnlineAuction.BLL.Interfaces
{
    public interface IUserManager : IManager<User>
    {
        User GetByEmail(string email);
    }
}
