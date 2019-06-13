using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wheresmymovies.entities;

namespace wheresmymovies.service.Validator
{
    public abstract class BaseValidator
    {
        protected bool ValidString(string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }

        protected bool ValidLength(string s, int l)
        {
            return s.Length <= l;
        }
    }

    public interface IMovieValidatorAsync
    {
        Task<Dictionary<string, List<Violation>>> Validate(Movie t, params string[] propertiesToExclude);
    }

    public class MovieValidatorAsync : BaseValidator, IMovieValidatorAsync
    {
        public Task<Dictionary<string, List<Violation>>> Validate(Movie movie, params string[] propertiesToExclude)
        {
            var violations = new Dictionary<string, List<Violation>>();
            var tasks = new List<Task>();

            if (!propertiesToExclude.Contains(nameof(MovieActor.Actor)))
            {
                var actorValidator = new MovieActorValidator();
                var actorTasks = new List<Task>();
                foreach(var actor in movie.Actors)
                {
                    actorTasks.Add(actorValidator.Validate(actor));
                }
                Task.WaitAll(actorTasks.ToArray());// NO! Dont do this.
            }
            throw new NotImplementedException();
        }
    }

    public class MovieActorValidator : BaseValidator, IValidator<MovieActor>
    {
        private const int MaxLength = 256;
        public Task<List<Violation>> Validate(MovieActor t, params string[] propertiesToExclude)
        {
            if (t == null) throw new ArgumentNullException();
            var violations = new List<Violation>();

            if (!propertiesToExclude.Contains(nameof(MovieActor.Actor)))
            {
                if (!ValidString(t.Actor))
                    violations.Add(new Violation(nameof(MovieActor.Actor), t.Actor, $"{nameof(MovieActor.Actor)} required."));
                
                if (!ValidLength(t.Actor, MaxLength))
                    violations.Add(new Violation(nameof(MovieActor.Actor), t.Actor, $"{nameof(MovieActor.Actor)} length exceeds {MaxLength} characters."));
            }

            return Task.FromResult(violations);
        }
    }
}