using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace wheresmymovies.entities
{
    public class MovieQuery : IQuery<Movie>
    {
        private readonly IList<IOperation<Movie>> _queryEntities;

        public MovieQuery()
        {
            _queryEntities = new List<IOperation<Movie>>();
        }

        public MovieQuery And(params IPropertyValue<Movie>[] entities)
        {
            var and = new And<Movie>();
            foreach(var entity in entities)
            {
                and.Add(entity.Property, entity.Values);
            }
            _queryEntities.Add(and);
            return this;
        }

        public MovieQuery Or(params IPropertyValue<Movie> entities)
        {
            var or = new Or<Movie>();
            foreach(var entity)
        }
    }

    internal interface IQueryEntity<T> { }

    internal interface IOperation<T> : IQueryEntity<T>
    { 
        public string Name { get; }
        IPropertyValue<T> PropertyValues { get; }

        public void Add(string key, object value);
    }

    internal class And<T> : IOperation<T>
    {
        public string Name { get; }

        private IPropertyValue<T> _propertyValue;
        IPropertyValue<T> PropertyValues => _propertyValue;

        public And()
        {
            Name = "And";
        }

        public void Add(string key, object value)
        {
            if (PropertyValues == null)
            {
                if (value == null)
                {
                    _propertyValue = new NullValue<T>(key);
                }
                else
                {
                    _propertyValue = new PropertyValue<T>(key, value);
                }
            }
            else if (value != null)
            {
                _propertyValue.Values.Add(value);
            }
        }
    }

    internal class Or<T> : IOperation<T>
    {
        public string Name { get; }

        private IPropertyValue<T> _propertyValue;
        IPropertyValue<T> PropertyValues => _propertyValue;

        public Or()
        {
            Name = "Or";
        }

        public void Add(string key, object value)
        {
            if (PropertyValues == null)
            {
                if (value == null)
                {
                    _propertyValue = new NullValue<T>(key);
                }
                else 
                {
                    _propertyValue = new PropertyValue<T>(key, value);
                }
            }
            else if (value != null)
            {
                _propertyValue.Values.Add(value);
            }
        }
    }

    internal interface IPropertyValue<T> : IQueryEntity<T>
    {
        public string Property { get; }
        public HashSet<object> Values { get; }
    }

    internal class PropertyValue<T> : IPropertyValue<T>
    {
        public string Property { get; }
        public HashSet<object> Values { get; }

        public PropertyValue(string property, object value)
        {
            Property = property;
            Values = new HashSet<object>();
            Values.Add(value);
        }
    }

    internal class NullValue<T> : IPropertyValue<T>
    {
        public string Property { get; }
        public HashSet<object> Values { get; }

        public NullValue(string property)
        {
            Property = property;
        }
    }
}