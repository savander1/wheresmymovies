using System.Net;

namespace wheresmymovies.Models
{
    public class SearchParameters
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public bool IsTitleSearch => !string.IsNullOrEmpty(Title);

        internal bool IsValid()
        {
            var idIsSet = !string.IsNullOrWhiteSpace(Id);
            var titleIsSet = !string.IsNullOrWhiteSpace(Title);

            return (idIsSet || titleIsSet) && !(idIsSet && titleIsSet);
        }

        public override string ToString()
        {
            return $"Id: [{Id}] Title: [{Title}]";
        }
    } 
}
