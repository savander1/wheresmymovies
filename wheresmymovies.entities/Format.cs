namespace wheresmymovies.entities
{
    public class Format
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static class Formats
    {
        public static readonly string Dvd = "Dvd";
        public static readonly string BluRay = "Bluray";
        public static readonly string Digital = "Digital";
    }
}
