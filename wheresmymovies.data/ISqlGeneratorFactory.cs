namespace wheresmymovies.data
{
    public interface ISqlGeneratorFactory
    {
        ISqlGenerator<T, TId> GetSqlGenerator<T, TId>();
    }
}
