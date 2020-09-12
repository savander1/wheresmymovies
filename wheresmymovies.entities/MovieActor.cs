using wheresmymovies.entities.Filter;

namespace wheresmymovies.entities
{
    public class MovieActor : IListProperty
    {
        public int Id { get;set; }
        public string Actor { get;set; }
    }
}
