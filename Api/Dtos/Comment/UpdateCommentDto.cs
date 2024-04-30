namespace Api.Dtos.Comment
{
    public class UpdateCommentDto
    {
        public int MovieEpisodeId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
