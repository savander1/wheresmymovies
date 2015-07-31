using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.shanevandermeer.wheresmymovies.Models
{
    public class Movie
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public ICollection<string> Genre { get; set; }
        public ICollection<string> Director { get; set; }
        public ICollection<string> Writer { get; set; }
        public ICollection<string> Actors { get; set; }
        public string Plot { get; set; }
        public ICollection<string> Language { get; set; }
        public string Country { get; set; }
        public string Poster { get; set; }
        public string ImdbID { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
    }
}
