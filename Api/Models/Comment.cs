namespace Api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set;} = DateTime.Now;
        
        public int MovieEpisodeId { get; set; }
        public MovieEpisode MovieEpisode { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
