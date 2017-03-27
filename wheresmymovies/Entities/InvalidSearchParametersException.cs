using System;

namespace wheresmymovies.Entities
{
    public class InvalidSearchParametersException : Exception
    {
        public InvalidSearchParametersException(string message): base(message)
        {

        }
    }
}
