namespace Api.Models
{
    public class MovieSeriesGenre
    {
        public int MovieSeriesId { get; set; }
        public int GenreId { get; set; }
        public MovieSeries MovieSeries { get; set; }
        public Genre Genre { get; set; }
    }
}
