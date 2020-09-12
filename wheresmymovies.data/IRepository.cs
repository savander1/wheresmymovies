using System.Collections.Generic;
using wheresmymovies.entities;
using wheresmymovies.entities.Filter;

namespace wheresmymovies.data
{
    public interface IRepository<T, TId>
    {
        T Get(TId id);
        T Save(T t);
        T Update(TId id, T t);
        void Delete(TId id);
        IEnumerable<T> Find(IFilter<T> query);
    }

    public interface IMovieRepository : IRepository<Movie, int>
    { }
}
