using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using wheresmymovies.Data;

namespace wheresmymovies
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInstance<IMovieRepository>(new MovieRepository());
            services.AddMvc();
            services.BuildServiceProvider();
        }
        
        public void Configure(IApplicationBuilder app)
        {
            app.RunIISPipeline();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "/api/{controller}/{action}");
            });
            app.Run(async (context) =>
            { 
                await context.Response.WriteAsync("Hello World!");
            });   
        }
    }
}