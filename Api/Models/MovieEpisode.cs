using System.Text.Json.Serialization;

namespace Api.Models
{
    public class MovieEpisode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EpisodeNumber { get; set; }
        public string VideoUrl { get; set; }
        public int Duration { get; set; }
        public int NumberofViewers { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
        public int MovieSeriesId { get; set; }
        
        public MovieSeries MovieSeries { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
