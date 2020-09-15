namespace wheresmymovies.api.Service.Mapping
{
    public interface IMapper<Tentity, Tmodel>
    {
        public Tentity ToEntity(Tmodel model);
        public Tmodel ToModel(Tentity entity);
    }
}
