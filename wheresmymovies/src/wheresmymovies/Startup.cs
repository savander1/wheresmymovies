using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
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
            services.AddMvc();
        }
        
        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseMvc();   
        }
    }
}
