namespace wheresmymovies.data
{
    public interface IEntityReaderFactory
    {
        IEntityReader<T> GetReader<T>();
    }
}
