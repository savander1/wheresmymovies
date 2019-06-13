namespace wheresmymovies.service.Validation
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
}