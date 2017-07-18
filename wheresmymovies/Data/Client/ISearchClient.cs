using Microsoft.Azure.Search.Models;
using System.Net;
using System.Threading.Tasks;
using wheresmymovies.Entities;

namespace wheresmymovies.Data.Client
{
    public interface ISearchClient
    {
        Task<DocumentIndexResult> AddAsync(Movie movie);
        Task<DocumentIndexResult> DeleteAsync(string id);
        Task<DocumentSearchResult<Movie>> GetAsync();
        Task<Movie> GetAsync(string id);
    }
}