using System.Collections.Generic;

namespace wheresmymovies.entities
{
    public class MovieQuery : IQuery<Movie>
    {
        private readonly Dictionary<string, string> _properties;
        private readonly Dictionary<string, string> _contains;
        private readonly Dictionary<string, ICollection<string>> _in;

        public MovieQuery()
        {
            _properties = new Dictionary<string, string>();
            _contains = new Dictionary<string, string>();
            _in = new Dictionary<string, ICollection<string>>();
        }

        public IQuery<Movie> Contains(string name, string value)
        {
            _contains.Add(name, value);
            return this;
        }

        public IQuery<Movie> Equals(string name, string value)
        {
            _properties.Add(name, value);
            return this;
        }

        public IQuery<Movie> In(string name, ICollection<string> collection)
        {
            _in.Add(name, collection);
            return this;
        }
    }
}