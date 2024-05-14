using Api.Dtos.Comment;
using Api.Models;

namespace Api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                AppUserId = commentModel.AppUserId,
                CreatedBy = commentModel.AppUser.Email,
                CreatedDate = commentModel.CreatedDate,
                MovieEpisodeId = commentModel.MovieEpisodeId,
            };
        }
    }
}
