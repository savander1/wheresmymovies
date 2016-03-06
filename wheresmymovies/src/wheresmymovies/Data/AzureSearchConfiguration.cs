namespace wheresmymovies.Data
{
    public class AzureSearchConfiguration
    {
        public string SearchEndpoint { get; private set; }
        public string ApiKey { get; private set; }

        public AzureSearchConfiguration(string apikey, string searchEndpoint)
        {
            ApiKey = apikey;
            SearchEndpoint = searchEndpoint;
        }
    }
}
