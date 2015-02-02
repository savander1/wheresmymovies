using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheresMyMovies.Data.Repository
{
    public interface IRepository<out T> where T : class
    {
        IQueryable<T> Get();
    }
}
