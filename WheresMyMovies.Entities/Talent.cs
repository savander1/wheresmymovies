
using System.Collections.Generic;
namespace WheresMyMovies.Entities
{
    public abstract class Talent
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual int Id { get; set; }
        public virtual TalentType Type { get; set; }
        public virtual IList<Movie> Movies { get; protected set; }
    }

    public class Actor : Talent 
    {
        public Actor()
        {
            Type = TalentType.Actor;
        }
    }

    public class Writer : Talent 
    {
        public Writer()
        {
            Type = TalentType.Writer;
        }
    }

    public class Director : Talent 
    {
        public Director()
        {
            Type = TalentType.Director;
        }
    }
}
