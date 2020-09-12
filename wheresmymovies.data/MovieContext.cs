using Microsoft.EntityFrameworkCore;
using wheresmymovies.entities;

namespace wheresmymovies.data
{
    public class MovieContext : DbContext
    {
        public MovieContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
            optionbuilder.UseSqlite(@"Data Source=c:\movie.db");
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
