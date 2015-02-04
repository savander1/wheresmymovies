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
        public virtual IList<Talent> Director { get; set; }
        public virtual IList<Talent> Writer { get; set; }
        public virtual IList<Talent> Actor { get; set; }
        public virtual string Plot { get; set; }
        public virtual string Language { get; set; }
        public virtual string Country { get; set; }
        public virtual byte[] Poster { get; set; }
        public virtual MovieType MovieType { get; set; }
    }

}
