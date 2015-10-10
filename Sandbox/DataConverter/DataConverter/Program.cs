using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using wheresmymovies.Entities;
using System;

namespace DataConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            var movies = GetMovies();
            var movieJson = JsonConvert.SerializeObject(movies, Formatting.Indented);
            WriteFile(movieJson);
        } 

        static IEnumerable<Movie> GetMovies()
        {
            var moviesString = ReadFile();

            var movies = JsonConvert.DeserializeObject<IEnumerable<MovieTEMP>>(moviesString);

            return movies.Select(x=> new Movie(x)).Distinct();
        } 

        static string ReadFile()
        {
            using (var stream = File.Open("data.json", FileMode.Open))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        } 

        static void WriteFile(string json)
        {
            using (var stream = File.CreateText("cleansed.json"))
            {
                stream.Write(json);
            }
        }
    }
}
