using System.Collections.Generic;
using System.Threading.Tasks;
using wheresmymovies.entities;

namespace wheresmymovies.service.Validation
{
    public interface IMovieValidatorAsync
    {
        Task<Dictionary<string, List<Violation>>> Validate(Movie t, params string[] propertiesToExclude);
    }
}