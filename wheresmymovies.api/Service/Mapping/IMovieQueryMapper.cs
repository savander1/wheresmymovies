using wheresmymovies.entities.Filter;

namespace wheresmymovies.api.Service.Mapping
{
    public interface IMovieQueryMapper : IMapper<Filter<entities.Movie>, Models.MovieQuery>
    {
    }
}
