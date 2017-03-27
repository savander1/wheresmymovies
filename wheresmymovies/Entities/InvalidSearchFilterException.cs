using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wheresmymovies.Entities
{
    public class InvalidSearchFilterException : Exception
    {
        public InvalidSearchFilterException(string message) :base(message)
        {

        }
    }
}
