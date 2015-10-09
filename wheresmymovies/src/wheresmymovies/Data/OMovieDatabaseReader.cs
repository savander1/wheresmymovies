using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Data
{
    public class OMovieDatabaseReader
    {
        public Movie GetMovie(MovieSearchParameters parameters)
        {
            var data = GetData(parameters);

            try
            {
                return JsonConvert.DeserializeObject<Movie>(data);
            }
            catch
            {
                return new Movie();
            }
        }
         
        private string GetData(MovieSearchParameters parameters)
        {
            var endPoint = GetEndpoint(parameters);
            
            var request = WebRequest.Create(endPoint);

            using (var response = request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        return  reader.ReadToEnd();
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
                builder.Append("?t=" + parameters.Name.Trim());
            }
            builder.Append("&plot=full&r=json");
            return new Uri(builder.ToString());
        }
    }
}