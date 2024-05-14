using Api.Data;
using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class ReactionRepository:IReactionRepository
    {
        private readonly MovieDbContext _context;
        public ReactionRepository(MovieDbContext context) 
        {
            _context = context;
        }

        public async Task<List<Reaction>>GetAllAsync()
        {
            return await _context.Reaction.ToListAsync();
        }

        public async Task<Reaction> LikeAsync(Reaction reaction)
        {
            var existingLike = await _context.Reaction.FirstOrDefaultAsync(e => e.AppUserId == reaction.AppUserId && e.MovieEpisodeId == reaction.MovieEpisodeId);

            if (existingLike == null)
            {
                reaction.IsLike = true;
                _context.Reaction.Add(reaction);
            }
            else
            {
                existingLike.IsLike = !existingLike.IsLike;
            }

            await _context.SaveChangesAsync();
            return reaction;
        }

        public async Task<Reaction> UnlikeAsync(Reaction reaction)
        {
            var existingLike = await _context.Reaction.FirstOrDefaultAsync(e => e.AppUserId == reaction.AppUserId && e.MovieEpisodeId == reaction.MovieEpisodeId);

            if (existingLike != null)
            {
                _context.Reaction.Remove(existingLike);
                await _context.SaveChangesAsync();
            }
            return reaction;
        }
    }
}
