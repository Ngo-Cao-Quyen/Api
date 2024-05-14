using Api.Data;
using Api.Helpers;
using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly MovieDbContext _context;

        public RegionRepository(MovieDbContext context)
        {
            _context = context;
        }

        public async Task<List<Region>> GetAll(QueryObject query)
        {
            var region = await _context.Region.ToListAsync();
            return region;
        }

        public async Task<Region> GetById(int id)
        {
            
            return await _context.Region.FindAsync(id);
        }

        public async Task<Region> GetByName(string name)
        {
            var region = await _context.Region.Where(e => e.Name.Trim().ToUpper() == name)
                .FirstOrDefaultAsync();
            return region;
        }

        public async Task<Region> GetSeriesByRegion(int regionId, [FromQuery] QueryObject query)
        {
            var region = await _context.Region.Include(e => e.Series).FirstOrDefaultAsync(e => e.Id == regionId);
            return region;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _context.Region.AddAsync(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Region region)
        {
            _context.Region.Remove(region);
            await _context.SaveChangesAsync();

            return region;
        }

        public async Task<Region> UpdateAsync(Region region)
        {
            _context.Region.Update(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<bool> RegionExists(int id)
        {
            return await _context.Region.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> RegionExistsName(string name)
        {
            return await _context.Region.AnyAsync(e => e.Name.ToUpper().Trim() == name.ToUpper().Trim());
        }
    }
}
