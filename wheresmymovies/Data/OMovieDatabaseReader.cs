using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Data
{
    public class OMovieDatabaseReader
    {
        public Movie GetMovie(MovieSearchParameters parameters)
        {
           return GetData(parameters).ContinueWith((antecedent) =>
           {
               try
               {
                   var data = antecedent.Result;
                   var oMovie = JsonConvert.DeserializeObject<Omovie>(data);
                   return new Movie(oMovie);
               }
               catch
               {
                   return new Movie();
               }
           }).Result;
        }
         
        private async Task<string> GetData(MovieSearchParameters parameters)
        {
            var endPoint = GetEndpoint(parameters);
            using (var client = new HttpClient())
            {
                using (var stream = await client.GetStreamAsync(GetEndpoint(parameters)))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        return await Task<string>.Factory.StartNew(() => reader.ReadToEnd());
                    }
                }           
            }
        }

        private Uri GetEndpoint(MovieSearchParameters parameters)
        {
            var builder = new StringBuilder("http://www.omdbapi.com/");
            if (!string.IsNullOrEmpty(parameters.Id))
            {
                builder.Append("?i=" + parameters.Id.Trim());
            }
            else
            {
                builder.Append("?t=" + parameters.Title.Trim());
            }
            builder.Append("&plot=full&r=json");
            return new Uri(builder.ToString());
        }
    }
}