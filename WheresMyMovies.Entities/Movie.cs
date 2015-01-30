using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheresMyMovies.Entities
{
    public class Movie
    {
        public string Title { get; set; }
        public DateTime Year {get; set;}
        public string Rating {get; set;}
        public string Released {get; set;}
        public ICollection<string> Genre {get; set;}
        public ICollection<Talent> Director {get; set;}
        public ICollection<Talent> Writer {get; set;}
        public ICollection<Talent> Actor {get; set;}
        public string Plot {get; set;}
        public string Language {get; set;}
        public string Country {get; set;}
        public byte[] Poster {get; set;}
        public MovieType MovieType {get; set;}
        public Format Format {get; set;}

    }

}
