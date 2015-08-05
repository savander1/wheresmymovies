using System.Collections.Generic;

namespace com.shanevandermeer.wheresmymovies.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(string Id);
        bool Update(string id, T t);
        bool Insert(T t);
        bool Delete(string id);
    }
}
