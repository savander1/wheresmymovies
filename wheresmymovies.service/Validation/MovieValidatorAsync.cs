using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wheresmymovies.entities;

namespace wheresmymovies.service.Validation
{

    public class MovieValidatorAsync : BaseValidator, IMovieValidatorAsync
    {
        public Task<Dictionary<string, List<Violation>>> Validate(Movie movie, params string[] propertiesToExclude)
        {
            var violations = new Dictionary<string, List<Violation>>();

            if (!propertiesToExclude.Contains(nameof(Movie.Actors)))
            {
                var actorValidator = new MovieActorValidator();
                var actorViolations = new List<Violation>();
                var actorTasks = new List<Task>();
                foreach(var actor in movie.Actors)
                {
                    var t = actorValidator.Validate(actor).ContinueWith((arg) => actorViolations.AddRange(arg.Result));
                    actorTasks.Add(t);
                }
                Task.WaitAll(actorTasks.ToArray());
                if (actorViolations.Any())
                    violations.Add(nameof(Movie.Actors), actorViolations);
            }

            //TODO: Figure out string length constants-> get rid of this magic number
            if (!propertiesToExclude.Contains(nameof(Movie.Description)) && ValidLength(movie.Description, 4000))
            {
                violations.Add(nameof(Movie.Description), new List<Violation>
                {
                    new Violation(nameof(Movie.Description), movie.Description, $"{nameof(Movie.Description)} length exceeds {4000} characters.")

                });
            }

            return Task.FromResult(violations);
        }

        private string[] GetCreateValidationParameters()
        {
            var parameters = new List<string>
            {

                nameof(Movie.Directors),
                nameof(Movie.FormatLocations),
                nameof(Movie.FullImgUrl),
                nameof(Movie.Genres),
                nameof(Movie.Runtime),
                nameof(Movie.ThumbImgUrl),
                nameof(Movie.Title),
                nameof(Movie.Writers),
                nameof(Movie.Years)
            };

            return parameters.ToArray();
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