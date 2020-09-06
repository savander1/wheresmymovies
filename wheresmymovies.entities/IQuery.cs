namespace wheresmymovies.entities
{
    public interface IQuery<T>
    {
        IQuery<T> WithProperty(string name, string value);
    }
}