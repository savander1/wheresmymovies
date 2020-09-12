using System.Linq;

namespace wheresmymovies.entities.Filter
{
    public interface IFilter<T>
    {
        IQueryable<T> ToQueryable();
        Filter<T> PartialMatch(string name, object value);
        Filter<T> ExactMatch(string name, object value);
    }
}