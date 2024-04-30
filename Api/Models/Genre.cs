namespace Api.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<MovieSeriesGenre> MovieSeriesGenres { get; set; } = new List<MovieSeriesGenre>();
    }
}
