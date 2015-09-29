using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using wheresmymovies.Entities;

namespace DataConverter
{
    class Program
    {
        static void Main(string[] args)
        {

        } 

        static IEnumerable<MovieTEMP> GetMovies()
        {
            var moviesString = ReadFileAsync().Result;

            var movies = JsonConvert.DeserializeObject<IEnumerable<MovieTEMP>>(moviesString);

            return movies.Select(x=> x.).Distinct();
        } 

        static Task<string> ReadFileAsync()
        {
            using (var stream = File.Open("", FileMode.Open))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEndAsync();
                }
            }
        } 
    }
}
