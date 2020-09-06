using System.Collections.Generic;
using wheresmymovies.entities;

namespace wheresmymovies.data
{
    public interface IRepository<T, TId>
    {
        T Get(TId id);
        T Save(T t);
        T Update(TId id, T t);
        void Delete(TId id);
        IEnumerable<T> Find(IQuery<T> query);
    }
}
