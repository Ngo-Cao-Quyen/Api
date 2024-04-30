namespace Api.Dtos.MovieEpisode
{
    public class UpdateMovieEpisodeDto
    {
        public string Name { get; set; }
        public int EpisodeNumber { get; set; }
        public string VideoUrl { get; set; }
        public int Duration { get; set; }
        public int NumberofViewers { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
        public int MovieSeriesId { get; set; }
    }
}
