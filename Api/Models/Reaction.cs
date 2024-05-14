namespace Api.Models
{
    public class Reaction
    {
        public int Id { get; set; }
        public bool IsLike { get; set; }
        public int MovieEpisodeId { get; set; }
        public MovieEpisode MovieEpisode { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
