using Api.Data;
using Api.Helpers;
using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;


namespace Api.Repository
{
    public class MovieEpisodeRepository : IMovieEpisodeRepository
    {
        private readonly MovieDbContext _context;

        public MovieEpisodeRepository(MovieDbContext context)
        {
            _context = context;
        }

        public async Task<MovieEpisode> CreateAsync(MovieEpisode movieEpisode)
        {
            await _context.MovieEpisode.AddAsync(movieEpisode);
            await _context.SaveChangesAsync();
            return movieEpisode;
        }

        public async Task<MovieEpisode> DeleteAsync(int id)
        {
            var movieEpisode = await _context.MovieEpisode.FindAsync(id);
            if (movieEpisode == null)
            {
                return null;
            }

            _context.MovieEpisode.Remove(movieEpisode);
            await _context.SaveChangesAsync();
            return movieEpisode;
        }

        public async Task<List<MovieEpisode>> GetAll(QueryObject query)
        {
            var movieEpisode = await _context.MovieEpisode.ToListAsync();
            return movieEpisode;
        }

        public async Task<MovieEpisode> GetById(int id)
        {
            var movieEpisode = await _context.MovieEpisode.Include(e => e.Comments).Include(e => e.Reactions).FirstOrDefaultAsync(e => e.Id == id);
            if (movieEpisode == null)
            {
                return null;
            }
            return movieEpisode;
        }

        public async Task<MovieEpisode> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> MovieEpisodeExists(int id)
        {
            return await _context.MovieEpisode.AnyAsync(e => e.Id == id);
        }

        public async Task<MovieEpisode> UpdateAsync(MovieEpisode movieEpisode)
        {
            _context.MovieEpisode.Update(movieEpisode);
            await _context.SaveChangesAsync();

            return movieEpisode;
        }

        public async Task<MovieEpisode> DeleteAsync(MovieEpisode movieEpisode)
        {
           
            _context.MovieEpisode.Remove(movieEpisode);
            await _context.SaveChangesAsync();

            return movieEpisode;
        }

        public async Task UpdateViewCountAsync(int episodeId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var episode = await _context.MovieEpisode.FindAsync(episodeId);
                if (episode != null)
                {
                    episode.NumberofViewers++;
                    await _context.SaveChangesAsync();
                }
                transaction.Commit();
            }
        }
    }
}
