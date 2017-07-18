namespace wheresmymovies.Data
{
    public class AzureSearchConfiguration
    {
        public string AdminApiKey { get; private set; }
        public string ApiKey { get; private set; }

        public AzureSearchConfiguration(string apikey, string adminApiKey)
        {
            ApiKey = apikey;
            AdminApiKey = adminApiKey;
        }
    }
}
