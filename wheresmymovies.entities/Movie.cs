using System;
using System.Collections.Generic;

namespace wheresmymovies.entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ThumbImgUrl { get; set; }
        public string Description { get; set; }
        public ICollection<MovieYear> Years { get; set; }
        public long Runtime { get; set; }
        public ICollection<MovieGenre> Genres { get; set; }
        public ICollection<MovieDirector> Directors { get; set; }
        public ICollection<MovieWriter> Writers { get; set; }
        public ICollection<MovieActor> Actors { get; set; }
        public string FullImgUrl { get; set; }
        public ICollection<MovieFormatLocation> FormatLocations { get; set; }
    }
}
