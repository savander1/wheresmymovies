
using System.Collections.Generic;
namespace WheresMyMovies.Entities
{
    public class Genre
    {
        public virtual int Id { get; set; }
        public virtual string Category { get; set; }
        public virtual IList<Movie> Movies { get; protected set; }
    }
}
