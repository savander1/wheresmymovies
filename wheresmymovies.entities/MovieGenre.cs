using wheresmymovies.entities.Filter;

namespace wheresmymovies.entities
{
    public class MovieGenre : IListProperty
    {
        public int Id { get;set; }
        public string Genre { get;set; }
    }
}
