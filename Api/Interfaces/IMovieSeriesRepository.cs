using Api.Helpers;
using Api.Models;

namespace Api.Interfaces
{
    public interface IMovieSeriesRepository
    {
        Task<List<MovieSeries>> GetAll(QueryObject query);
        Task<MovieSeries>GetById(int id);
        /*Task<MovieSeries> GetEpisodeBySeriesId(int seriesId);*/
        Task<List<MovieSeries>> GetByName(string name);
        Task<MovieSeries>CreateAsync(MovieSeries movieSeries);
        Task<MovieSeries>UpdateAsync(MovieSeries movieSeries);
        Task<MovieSeries>DeleteAsync(MovieSeries movieSeries);
        Task<bool> MovieSeriesExists(int id);
        Task<bool> MovieSeriesExistsName(string name);
        Task<string> SaveImageAsync(IFormFile fileImage);
        void CalculateTotalView(List<MovieSeries> movieSeries);
    }
}
