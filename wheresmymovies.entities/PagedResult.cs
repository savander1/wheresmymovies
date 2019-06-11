using System.Collections.Generic;

namespace wheresmymovies.entities
{
    public class PagedResult<T> where T : class
    {
        public IList<T> Results {get;set;}
        public int TotalResults {get;set;}
    }
}