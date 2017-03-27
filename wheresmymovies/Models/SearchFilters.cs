using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wheresmymovies.Models
{
    public class SearchFilters
    {
        public string Title { get; set; }

        internal bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Title);
        }
    }
}
