using wheresmymovies.entities.Filter;

namespace wheresmymovies.entities
{
    public class MovieFormatLocation : IListProperty
    {
        public int Id {get;set;}
        public string Format {get;set;}
        public string Location {get; set; }
    }
}
