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
        public virtual string Released { get; set; }
        public virtual ICollection<string> Genre { get; set; }
        public virtual ICollection<Talent> Director { get; set; }
        public virtual ICollection<Talent> Writer { get; set; }
        public virtual ICollection<Talent> Actor { get; set; }
        public virtual string Plot { get; set; }
        public virtual string Language { get; set; }
        public virtual string Country { get; set; }
        public virtual byte[] Poster { get; set; }
        public virtual MovieType MovieType { get; set; }
    }

}
