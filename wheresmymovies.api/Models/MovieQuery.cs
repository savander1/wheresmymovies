using System.Collections.Generic;
using System.Linq;

namespace wheresmymovies.api.Models
{
    public class MovieQuery
    {
        public Movie Movie { get; }
        
        public MovieQuery(Movie movie)
        {
            Movie = movie;
        }

        public bool IsSet(string propertyName)
        {
            if (Movie == null) return false;

            var property = typeof(Movie).GetProperty(propertyName);
            var propertyValue = property.GetValue(Movie);
            var propertyType = property.GetType();

            if (propertyType == typeof(string))
            {
                return !string.IsNullOrEmpty((string?)propertyValue);
            }

            if (propertyType == typeof(int) || propertyType == typeof(long))
            {
                return (int)propertyValue != 0;
            }

            if (propertyType.GetInterfaces().Any(i => i.Name.Equals(nameof(IList<string>))))
            {
                return propertyValue != null && ((IList<string>)propertyValue).Any();
            }

            return false;
        }

    }
}
