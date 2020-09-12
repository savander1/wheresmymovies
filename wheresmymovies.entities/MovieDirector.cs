using wheresmymovies.entities.Filter;

namespace wheresmymovies.entities
{
    public class MovieDirector : IListProperty
    {
        public int Id { get;set; }
        public string Director { get;set; }
    }
}
