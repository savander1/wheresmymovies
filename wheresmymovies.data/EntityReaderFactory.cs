using System;
using wheresmymovies.entities;

namespace wheresmymovies.data
{
    internal class EntityReaderFactory : IEntityReaderFactory
    {
        public IEntityReader<T> GetReader<T>()
        {
            if (typeof(T) == typeof(Movie))
            {
                return new MovieReader() as IEntityReader<T>;
            }

            throw new InvalidOperationException();
        }
    }
}
