using System.Collections.Generic;
using System.Linq;

namespace wheresmymovies.entities.Filter
{
    public class Filter<T> : IFilter<T>
    {
        private readonly IQueryable<T> _queryable;

        public Filter(IQueryable<T> queryable)
        {
            _queryable = queryable ?? throw new System.ArgumentNullException(nameof(queryable));
        }

        public Filter<T> PartialMatch(string name, object value)
        {
            var info = typeof(T).GetProperty(name);

            if (info.PropertyType.GetInterfaces().Any(x => x.GetType() == typeof(ICollection<IListProperty>)))
            {
                _queryable.Where(x => ((ICollection<IListProperty>)info.GetValue(x)).Any(x => x.ToString().IndexOf(value.ToString()) != -1));
            }
            else
            {
                _queryable.Where(x => info.GetValue(x).ToString().IndexOf(value.ToString()) != -1);
            }

            return this;
        }

        public Filter<T> ExactMatch(string name, object value)
        {
            var info = typeof(T).GetProperty(name);

            if (info.PropertyType.GetInterfaces().Any(x => x.GetType() == typeof(ICollection<IListProperty>)))
            {
                _queryable.Where(x => ((ICollection<IListProperty>)info.GetValue(x)).Any(x => x.Equals(value)));
            }
            else
            {
                _queryable.Where(x => info.GetValue(x).ToString().Equals(value));
            }

            return this;
        }

        public IQueryable<T> ToQueryable()
        {
            return _queryable;
        }
    }
}