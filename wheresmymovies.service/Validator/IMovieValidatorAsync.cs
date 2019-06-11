using System.Threading.Tasks;
using wheresmymovies.entities;

namespace wheresmymovies.service.Validator
{
    public interface IMovieValidatorAsync
    {
        Task Validate(Movie movie, params string[] propertiesToValidate);
    }
}