using Api.Models;

namespace Api.Interfaces
{
    public interface IReactionRepository
    {
        Task<List<Reaction>> GetAllAsync();
        Task<Reaction> LikeAsync(Reaction reaction);
        Task<Reaction> UnlikeAsync(Reaction reaction);
    }
}
