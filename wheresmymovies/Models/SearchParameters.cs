using System.Net;

namespace wheresmymovies.Models
{
    public class SearchParameters
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public SearchParameters Decode()
        {
            Title = WebUtility.UrlDecode(Title);

            return this;
        }

        internal bool IsValid()
        {
            var idIsSet = !string.IsNullOrWhiteSpace(Id);
            var titleIsSet = !string.IsNullOrWhiteSpace(Title);

            return (idIsSet || titleIsSet) && !(idIsSet && titleIsSet);
        }
    } 
}
