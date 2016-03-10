using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            var apikey = "4559E002B817ECFCE1BE91F698620F10";//"foo";
            var urlFormatter = Configuration.Get<string>("Data:search:searchUrl");
            var index = Configuration.Get<string>("Data:search:indexName");
            var apiVersion = Configuration.Get<string>("Data:search:apiVersion");

            var configuration = new AzureSearchConfiguration(apikey, string.Format(urlFormatter, index, apiVersion));
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole();
            services.AddInstance<IMovieRepository>(new MovieRepository(configuration, loggerFactory));

            var oMovieUrl = Configuration.Get<string>("Data:omovieUrl");
            services.AddInstance<IMetaDataSearchRepository>(new MetaDataSearchRepository (oMovieUrl));

            services.AddMvc();
        }
        
        public void Configure(IApplicationBuilder  app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(minLevel: LogLevel.Verbose);

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();  
        }

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
