using System.Collections.Generic;

namespace wheresmymovies.entities
{
    public class MovieQuery : IQuery<Movie>
    {
        private readonly Dictionary<string, string> _properties;

        public MovieQuery()
        {
            _properties = new Dictionary<string, string>();
        }

        public IQuery<Movie> WithProperty(string name, string value)
        {
            _properties.Add(name, value);
            return this;
        }
    }
}