using Api.Models;

namespace Api.Interfaces
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAll();
        Task<Region> GetById(int id);
        Task<Region> GetSeriesByRegion(int regionId);
        Task<Region> GetByName(string name);
        Task<Region> CreateAsync(Region region);
        Task<Region> UpdateAsync(Region region);
        Task<Region> DeleteAsync(Region region);
        Task<bool> RegionExists(int id);
    }
}
