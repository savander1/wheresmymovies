namespace wheresmymovies.Entities
{
    public class AzureMovies
    {
        public AzureMovie[] Value { get; set; }

        public AzureMovies() { }
        public AzureMovies(AzureMovie movie)
        {
            Value = new[] { movie };
        }
    }
}
