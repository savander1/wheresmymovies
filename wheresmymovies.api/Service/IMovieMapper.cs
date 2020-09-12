namespace wheresmymovies.api.Service
{
    public interface IMovieMapper
    {
        public entities.Movie ToEntity(Models.Movie model);
        public Models.Movie ToModel(entities.Movie entity);
    }

    public class MovieMapper : IMovieMapper
    {
        public entities.Movie ToEntity(Models.Movie model)
        {
            throw new System.NotImplementedException();
        }

        public Models.Movie ToModel(entities.Movie entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
