using System;
using wheresmymovies.entities;

namespace wheresmymovies.data
{
    internal class SqlGeneratorFactory : ISqlGeneratorFactory
    {
        public ISqlGenerator<T, TId> GetSqlGenerator<T, TId>()
        {
            if (typeof(T) == typeof(Movie))
            {
                return (ISqlGenerator<T, TId>)new MovieSqlGenerator();
            }

            throw new InvalidOperationException();
        }
    }
}
