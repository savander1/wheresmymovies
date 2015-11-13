using System;

namespace wheresmymovies.Entities
{
    public class MovieNotFoundException : Exception
    {
        public MovieNotFoundException():base("The movie you are looking for cannot be found")
        {

        }
    }
}
