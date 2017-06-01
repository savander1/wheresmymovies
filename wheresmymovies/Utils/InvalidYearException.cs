using System;

namespace wheresmymovies.Utils
{
    public class InvalidYearException : Exception
    {
        public InvalidYearException() : base("Not a valid vear")
        {
        }

        public InvalidYearException(int date) : base($"{date} is not a valid year")
        {
        }
    }
}
