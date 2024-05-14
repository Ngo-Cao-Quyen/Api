namespace Api.Dtos.Reaction
{
    public class ReactionDto
    {
        public bool IsLike { get; set; }
        public string AppUserId { get; set; }
        public int MovieEpisodeId { get; set; }
    }
}
