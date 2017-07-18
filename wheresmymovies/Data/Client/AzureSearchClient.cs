using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Threading.Tasks;
using wheresmymovies.Entities;

namespace wheresmymovies.Data.Client
{
    public class AzureSearchClient : ISearchClient
    {
        private readonly string _apiKey;
        private readonly string _adminKey;

        private ISearchIndexClient _azure;
        public ISearchIndexClient Azure
        {
            get
            {
                if (_azure == null)
                {
                    _azure = new SearchIndexClient("wheresmymovies", "movies", new SearchCredentials(_apiKey));
                }
                return _azure;
            }
            set
            {
                _azure = value;
            }
        }

        private ISearchIndexClient _admin;
        public ISearchIndexClient Admin
        {
            get
            {
                if (_admin == null)
                {
                    _admin = new SearchIndexClient("wheresmymovies", "movies", new SearchCredentials(_adminKey));
                }
                return _admin;
            }
            set
            {
                _admin = value;
            }
        }

        public AzureSearchClient(AzureSearchConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            _apiKey = configuration.ApiKey;
            _adminKey = configuration.AdminApiKey;
        }
        
        public async Task<DocumentSearchResult<Movie>> GetAsync()
        {
            return await Azure.Documents.SearchAsync<Movie>("*");
        }

        public async Task<DocumentIndexResult> AddAsync(Movie movie)
        {
            var action = IndexAction.MergeOrUpload(movie);
            return await Admin.Documents.IndexAsync(new IndexBatch<Movie>(new[]
            {
                action
            }));
        }
        
        public async Task<Movie> GetAsync(string id)
        {
            return await Azure.Documents.GetAsync<Movie>(id);
        }

        public async Task<DocumentIndexResult> DeleteAsync(string id)
        {
            var movie = await GetAsync(id);
            var action = IndexAction.Delete(movie);
            return await Admin.Documents.IndexAsync(new IndexBatch<Movie>(new[]
            {
                action
            }));
        }
    }
}
