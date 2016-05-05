using System;
using System.Collections.Generic;

namespace OnlineAuction.DAL.Interfaces
{
    public interface IRepository<T> : IDisposable
    {
        void Add(T record);
        List<T> GetList();
        T Get(int id);
        void Update(T record);
        void Remove(int id);
    }
}
