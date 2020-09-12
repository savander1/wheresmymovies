using wheresmymovies.entities.Filter;

namespace wheresmymovies.entities
{
    public class MovieWriter : IListProperty
    {
        public int Id { get;set; }
        public string Writer { get;set; }
    }
}
