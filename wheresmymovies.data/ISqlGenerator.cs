using System.Collections.Generic;
using System.Data;
using wheresmymovies.entities;

namespace wheresmymovies.data
{
    public interface ISqlGenerator<T, TId>
    {
        IDbConnection connection { set; }
        IDbCommand GenerateDelete(TId id);
        IDbCommand GenerateFind(IQuery<T> query);
        IDbCommand GenerateGet(TId id);
        IDbCommand GenerateSave(T t);
        IDbCommand GenerateUpdate(T original, T t);
    }
}
