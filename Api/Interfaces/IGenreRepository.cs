using Api.Models;

namespace Api.Interfaces
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetAll();
        Task<Genre> GetById(int id);
        /*Task<Genre> GetByName(string name);*/
        Task<List<MovieSeries>> GetMovieSeriesByGenre(int genreId);
        Task<Genre> CreateAsync(Genre genre);
        Task<Genre> UpdateAsync(Genre genre);
        Task<Genre> DeleteAsync(Genre genre);
        Task<bool> GenreExists(int id);
    }
}