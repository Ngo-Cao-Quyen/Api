using Api.Models;

namespace Api.Interfaces
{
    public interface IMovieSeriesRepository
    {
        Task<List<MovieSeries>> GetAll();
        Task<MovieSeries>GetById(int id);
        Task<MovieSeries> GetEpisodeBySeriesId(int seriesId);
        Task<MovieSeries>GetByName(string name);
        Task<MovieSeries>CreateAsync(MovieSeries movieSeries);
        Task<MovieSeries>UpdateAsync(MovieSeries movieSeries);
        Task<MovieSeries>DeleteAsync(MovieSeries movieSeries);
        Task<bool> MovieSeriesExists(int id);
        Task<string> SaveImageAsync(IFormFile fileImage);
    }
}
