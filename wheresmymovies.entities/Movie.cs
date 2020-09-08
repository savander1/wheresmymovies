using System.Collections.Generic;

namespace wheresmymovies.entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ThumbImgUrl { get; set; }
        public string Description { get; set; }
        public ICollection<MovieYear> Years { get; set; } = new List<MovieYear>();
        public long Runtime { get; set; }
        public ICollection<MovieGenre> Genres { get; set; } = new List<MovieGenre>();
        public ICollection<MovieDirector> Directors { get; set; } = new List<MovieDirector>();
        public ICollection<MovieWriter> Writers { get; set; } = new List<MovieWriter>();
        public ICollection<MovieActor> Actors { get; set; } = new List<MovieActor>();
        public string FullImgUrl { get; set; }
        public ICollection<MovieFormatLocation> FormatLocations { get; set; } = new List<MovieFormatLocation>();
    }
}
