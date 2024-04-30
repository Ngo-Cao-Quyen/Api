using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Api.Models
{
    public class MovieSeries
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? PosterPath { get; set; }
        public int TotalEpisode { get; set; }
        public int TotalNumberofViewers { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; }
        public int YearId { get; set; }
        public Year Year { get; set; }
        public int[] GenreIds { get; set; }

        public List<MovieEpisode> Episodes { get; set; } = new List<MovieEpisode>();
        public List<MovieSeriesGenre> MovieSeriesGenres { get; set; } = new List<MovieSeriesGenre>();
    }
}
