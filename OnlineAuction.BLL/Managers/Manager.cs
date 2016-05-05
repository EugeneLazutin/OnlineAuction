using OnlineAuction.DAL.Interfaces;

namespace OnlineAuction.BLL.Managers
{
    public class Manager<TE> : AbstractManager<TE> where TE : class
    {
        public Manager(IRepository<TE> repository) : base(repository)
        {
        }
    }
}
