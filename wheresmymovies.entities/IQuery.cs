using System.Collections.Generic;

namespace wheresmymovies.entities
{
    public interface IQuery<T>
    {
        IQuery<T> Equals(string name, string value);
        IQuery<T> Contains(string name, string value);
        IQuery<T> In(string name, ICollection<string> collection);
    }
}