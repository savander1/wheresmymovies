using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using wheresmymovies.Data;

namespace wheresmymovies
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }
    	public Startup(IApplicationEnvironment applicationEnvironment, IRuntimeEnvironment runtimeEnvironment)
	    {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("config.json");

            Configuration = builder.Build();
        }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var apikey = "foo";
            services.AddInstance<IMovieRepository>(new MovieRepository(apikey));

            var oMovieUrl = Configuration.Get<string>("Data:omovieUrl");
            services.AddInstance<IMetaDataSearchRepository>(new MetaDataSearchRepository (oMovieUrl));

            services.AddMvc();
        }
        
        public void Configure(IApplicationBuilder  app)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();  
        }

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
