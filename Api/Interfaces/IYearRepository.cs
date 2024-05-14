using Api.Helpers;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Interfaces
{
    public interface IYearRepository
    {
        Task<List<Year>> GetAll(QueryObject query);
        Task<Year> GetById(int id);
        Task<Year> GetByName(string name);
        Task<Year> GetSeriesByYear(int yearId, [FromQuery] QueryObject query);
        Task<Year> CreateAsync(Year year);
        Task<Year> UpdateAsync(Year year);
        Task<Year> DeleteAsync(Year year);
        Task<bool> YearExists(int id);
        Task<bool> YearExistsName(int name);
    }
}
