using Microsoft.EntityFrameworkCore;

namespace wheresmymovies.data
{
    public interface IDbContextFactory<T> where T : DbContext
    {
        T GetContext();
    }
}