using Api.Data;
using Api.Dtos.Genre;
using Api.Dtos.MovieSeries;
using Api.Helpers;
using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Data;


namespace Api.Repository
{
    public class MovieSeriesRepository : IMovieSeriesRepository
    {
        private readonly MovieDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MovieSeriesRepository(MovieDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<List<MovieSeries>> GetAll(QueryObject query)
        {
            var movieSeries = _context.MovieSeries.Include(e => e.Episodes).AsQueryable();   

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            
            return await movieSeries.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

       

        public async Task<MovieSeries> GetById(int id) 
        {
            var movieSeries = await _context.MovieSeries.Include(e => e.Episodes).FirstOrDefaultAsync(e => e.Id == id);
                         
            return movieSeries;
        }

       /* public async Task<MovieSeries> GetEpisodeBySeriesId(int movieSeriesId)
        {
            return await _context.MovieSeries.Include(e => e.Episodes).FirstOrDefaultAsync(e => e.Id == movieSeriesId);
        }*/

        public async Task<List<MovieSeries>>GetByName(string name)
        {
            var movieSeries = await _context.MovieSeries.Where(e => e.Name.Contains(name)).Include(e => e.Episodes).ToListAsync();

            return movieSeries;
        }

       public async Task<MovieSeries>CreateAsync(MovieSeries movieSeries)
        {
            await _context.MovieSeries.AddAsync(movieSeries);
            await _context.SaveChangesAsync();
            return movieSeries;
        }

        public async Task<MovieSeries>DeleteAsync(MovieSeries movieSeries)
        {
    
            _context.MovieSeries.Remove(movieSeries);
            await _context.SaveChangesAsync();

            return movieSeries;

        }

        public async Task<MovieSeries>UpdateAsync(MovieSeries movieSeries)
        { 
            _context.MovieSeries.Update(movieSeries);
            await _context.SaveChangesAsync();

            return movieSeries;
        }

        public async Task<bool> MovieSeriesExists(int id)
        {
            var existingSeries = await _context.MovieSeries.AnyAsync(e => e.Id == id);
            return existingSeries;
        }

        public async Task<bool> MovieSeriesExistsName(string name)
        {
            var existingNameSeries = await _context.MovieSeries.AnyAsync(e => e.Name.ToUpper().Trim() == name.ToUpper().Trim());
            return existingNameSeries;
        }

        public async Task<string> SaveImageAsync(IFormFile fileImage)
        {
            var fileProvider = Directory.GetCurrentDirectory() + "\\Images\\";
            string fileName = "Image" + Path.GetRandomFileName() + Path.GetExtension(fileImage.FileName);
            
            var filePath = Path.Combine("" + fileProvider, fileName);
            if(Directory.Exists(filePath) == false)
            {
                Directory.CreateDirectory(fileProvider);
            }
            /*if (!IsImage(fileImage))
            {
                throw new Exception("Định dạng file không hợp lệ. Chỉ hỗ trợ các định dạng: jpg, jpeg, png, gif");

            }*/
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileImage.CopyToAsync(stream);
            }
            
            return fileName;
        }

        private bool IsImage(IFormFile fileImage)
        {
            return fileImage.ContentType.Contains("image/png")
                && fileImage.ContentType.Contains("image/jpg");

        }

        public void CalculateTotalView(List<MovieSeries> movieSeries)
        {
            foreach (var series in movieSeries)
            {
                series.TotalNumberofViewers = series.Episodes.Sum(e => e.NumberofViewers);
            }

        }
    }
}
