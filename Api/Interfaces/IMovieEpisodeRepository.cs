using Api.Models;

namespace Api.Interfaces
{
    public interface IMovieEpisodeRepository
    {
        Task<List<MovieEpisode>> GetAll();
        Task<MovieEpisode> GetById(int id);
        Task<MovieEpisode> GetByName(string name);
        Task<MovieEpisode> CreateAsync(MovieEpisode movieEpisode);
        Task<MovieEpisode> UpdateAsync(MovieEpisode movieEpisode);
        Task<MovieEpisode> DeleteAsync(MovieEpisode movieEpisode);
        Task<bool> MovieEpisodeExists(int id);
        Task UpdateViewCountAsync(int seriesId);
    }
}
