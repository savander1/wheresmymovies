namespace wheresmymovies.Models
{
    public class MovieSearchParameters
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public MovieSearchParameters Decode()
        {
            Name = System.Net.WebUtility.UrlDecode(Name);

            return this;
        }
    } 
}
