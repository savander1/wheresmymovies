using wheresmymovies.entities.Filter;

namespace wheresmymovies.api.Service.Mapping
{
    public class MovieQueryMapper : IMovieQueryMapper
    {
        public Filter<entities.Movie> ToEntity(Models.MovieQuery model)
        {
            throw new System.NotImplementedException();
        }

        public Models.MovieQuery ToModel(Filter<entities.Movie> entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
