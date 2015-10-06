using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using wheresmymovies.Data;

namespace wheresmymovies
{
    public class Startup
    {
    	public Startup(IHostingEnvironment env)
	    {
            
        }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInstance<IMovieRepository>(new MovieRepository());
        }
        
        public void Configure(IApplicationBuilder  app)
        {
            var configuration = new ConfigurationBuilder()
                                .AddJsonFile("config.json");
            configuration.Build();


            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();  
        }
    }
}
