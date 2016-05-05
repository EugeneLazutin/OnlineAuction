using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using OnlineAuction.DAL.Interfaces;

namespace OnlineAuction.DAL.Repositories
{
    public abstract class AbstractRepository<TC, TE> : IRepository<TE> where TC : DbContext , new() where TE : class
    {
        private TC _db;

        protected AbstractRepository()
        {
            _db = new TC();
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }
        }

        public virtual void Add(TE record)
        {
            _db.Set<TE>().Add(record);
            _db.SaveChanges();
        }

        

        public virtual TE Get(int id)
        {
            return _db.Set<TE>().Find(id);
        }

        public virtual List<TE> GetList()
        {
            return _db.Set<TE>().ToList();
        }

        public virtual void Remove(int id)
        {
            var entities = _db.Set<TE>();
            var item = entities.Find(id);

            if (item != null)
            {
                entities.Remove(item);
                _db.SaveChanges();
            }
            else
            {
                throw new ObjectNotFoundException($"record not found: id = {id}");
            }
        }

        public virtual void Update(TE record)
        {
            _db.Entry(record).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
