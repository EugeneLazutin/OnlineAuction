using System.Collections.Generic;

namespace OnlineAuction.BLL.Interfaces
{
    public interface IManager<T>
    {
        void Add(T item);
        List<T> Get();
        T Get(int id);
        void Update(T item);
        void Remove(int id);
    }
}
