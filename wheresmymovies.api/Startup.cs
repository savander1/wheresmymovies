using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using wheresmymovies.api.Service;
using wheresmymovies.api.Service.Mapping;
using wheresmymovies.data;
using wheresmymovies.entities;

namespace wheresmymovies.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IMovieMapper, MovieMapper>();
            services.AddTransient<IMovieQueryMapper, MovieQueryMapper>();
            services.AddTransient<IMovieRepository, MovieRepository>();

            services.AddDbContext<MovieContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString(nameof(MovieContext))));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseErrorHandler();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
