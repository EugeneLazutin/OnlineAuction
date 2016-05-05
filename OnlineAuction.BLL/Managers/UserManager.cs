using System.Linq;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.DAL.Interfaces;
using OnlineAuction.Entities;

namespace OnlineAuction.BLL.Managers
{
    public class UserManager : AbstractManager<User>, IUserManager
    {
        public User GetByEmail(string email)
        {
            return Get().FirstOrDefault(x => x.Email == email);
        }

        public UserManager(IRepository<User> repository) : base(repository)
        {
        }
    }
}
