using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using wheresmymovies.Entities;

namespace wheresmymovies.Data.Client
{
    public interface ISearchClient
    {
        Task<HttpStatusCode> AddAsync(Movie movie);
        Task<HttpStatusCode> DeleteAsync(string id);
        Task<HttpResponseMessage> GetAsync();
        Task<Movie> GetAsync(string id);
    }
}