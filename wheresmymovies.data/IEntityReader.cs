using System.Collections.Generic;
using System.Data;

namespace wheresmymovies.data
{
    public interface IEntityReader<T>
    {
        T Read(IDataReader reader);
        IEnumerable<T> ReadList(IDataReader reader);
    }
}