using System.Collections.Generic;

namespace wheresmymovies.entities
{
    public class MovieQuery
    {
        private readonly Dictionary<string, string> _properties;
        public MovieQuery()
        {
            _properties = new Dictionary<string, string>();
        }

        public MovieQuery WithProperty(string name, string value)
        {
            _properties.Add(name, value);
            return this;
        }
    }
}