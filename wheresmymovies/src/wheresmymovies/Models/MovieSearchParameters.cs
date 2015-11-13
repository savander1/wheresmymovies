using System.Net;

namespace wheresmymovies.Models
{
    public class MovieSearchParameters
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public MovieSearchParameters Decode()
        {
            Name = WebUtility.UrlDecode(Name);

            return this;
        }
    } 
}
