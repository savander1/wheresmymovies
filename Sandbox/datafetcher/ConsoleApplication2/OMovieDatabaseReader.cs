using System;
using System.IO;
using System.Net;
using System.Text;

namespace ConsoleApplication2
{
    public class OMovieDatabaseReader
    {
        public string GetData(string line)
        {
            var endPoint = GetEndpoint(line);

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

        private Uri GetEndpoint(string line)
        {
            var builder = new StringBuilder("http://www.omdbapi.com/");
            if (line.StartsWith("tt", StringComparison.Ordinal))
            {
                builder.Append("?i=");
            }
            else
            {
                builder.Append("?t=");
            }
            builder.Append(line.Trim());
            builder.Append("&plot=full&r=json");
            return new Uri(builder.ToString());
        }
    }
}