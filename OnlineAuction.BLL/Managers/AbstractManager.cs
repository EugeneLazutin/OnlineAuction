using System.Collections.Generic;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.DAL.Interfaces;

namespace OnlineAuction.BLL.Managers
{
    public abstract class AbstractManager<TE> : IManager<TE> where TE : class
    {
        private readonly IRepository<TE> _repository;

        protected AbstractManager(IRepository<TE> repository)
        {
            _repository = repository;   
        }

        public virtual void Add(TE item)
        {
            _repository.Add(item);
        }

        public virtual List<TE> Get()
        {
            return _repository.GetList();
        }

        public virtual TE Get(int id)
        {
            return _repository.Get(id);
        }

        public virtual void Update(TE item)
        {
            _repository.Update(item);
        }

        public virtual void Remove(int id)
        {
            _repository.Remove(id);
        }
    }
}
