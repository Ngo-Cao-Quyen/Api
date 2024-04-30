using Api.Models;

namespace Api.Interfaces
{
    public interface IYearRepository
    {
        Task<List<Year>> GetAll();
        Task<Year> GetById(int id);
        Task<Year> GetByName(string name);
        Task<Year> GetSeriesByYear(int yearId);
        Task<Year> CreateAsync(Year year);
        Task<Year> UpdateAsync(Year year);
        Task<Year> DeleteAsync(Year year);
        Task<bool> YearExists(int id);
    }
}
