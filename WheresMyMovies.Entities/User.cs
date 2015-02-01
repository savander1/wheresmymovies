
namespace WheresMyMovies.Entities
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string Token { get; set; }
        public virtual Role UserRole { get; set; }
    }
}
