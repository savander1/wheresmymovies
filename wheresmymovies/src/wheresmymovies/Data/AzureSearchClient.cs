using Microsoft.Framework.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using wheresmymovies.Entities;

namespace wheresmymovies.Data
{
    public class AzureSearchClient
    {
        private static readonly string SearchUrl;

        static AzureSearchClient()
        {
            var urlFormatter = Startup.Configuration.Get<string>("Data:search:searchUrl");
            var index = Startup.Configuration.Get<string>("Data:search:indexName");
            var apiVersion = Startup.Configuration.Get<string>("Data:search:apiVersion");
            SearchUrl = string.Format(urlFormatter, index, apiVersion);
        }

    	public AzureSearchClient()
        {

        }

        public async Task<bool> Add(Movie movie)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(SearchUrl, GetHttpContent(movie));
                return response.IsSuccessStatusCode;
            }
        }

        private HttpContent GetHttpContent(Movie movie)
        {
            var content = 
            return content;
        }
    }
}
