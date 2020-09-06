using wheresmymovies.entities;

namespace wheresmymovies.data
{
    public class MovieRepository : Repository<Movie, int>
    {
        public MovieRepository(string connectionString) : base(connectionString)
        {
        }
    }
}
