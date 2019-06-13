using System.Collections.Generic;
using System.Threading.Tasks;

namespace wheresmymovies.service.Validator
{
    public interface IValidator<T> where T : class
    {
        Task<List<Violation>> Validate(T t, params string[] propertiesToExclude);
    }
}