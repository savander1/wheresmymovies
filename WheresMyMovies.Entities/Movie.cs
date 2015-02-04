using System;
using System.Collections.Generic;

namespace WheresMyMovies.Entities
{
    public class Movie
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual DateTime Year { get; set; }
        public virtual string Rating { get; set; }
        public virtual IList<Genre> Genre { get; protected set; }
        public virtual IList<Director> Director { get; set; }
        public virtual IList<Writer> Writer { get; set; }
        public virtual IList<Actor> Actor { get; set; }
        public virtual string Plot { get; set; }
        public virtual string Language { get; set; }
        public virtual string Country { get; set; }
        public virtual byte[] Poster { get; set; }
        public virtual MovieType MovieType { get; set; }
        public virtual string Location { get; set; }
    }

}
