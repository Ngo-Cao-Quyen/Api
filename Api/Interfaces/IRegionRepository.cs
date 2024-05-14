using Api.Helpers;
using Api.Models;

namespace Api.Interfaces
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAll(QueryObject query);
        Task<Region> GetById(int id);
        Task<Region> GetSeriesByRegion(int regionId, QueryObject query);
        Task<Region> CreateAsync(Region region);
        Task<Region> UpdateAsync(Region region);
        Task<Region> DeleteAsync(Region region);
        Task<bool> RegionExists(int id);
        Task<bool> RegionExistsName(string name);
    }
}
