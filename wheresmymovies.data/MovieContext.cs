using Microsoft.EntityFrameworkCore;
using wheresmymovies.entities;

namespace wheresmymovies.data
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
