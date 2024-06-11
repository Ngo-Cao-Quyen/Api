using Microsoft.AspNetCore.Mvc;
using Api.Interfaces;
using Api.Repository;
using Api.Models;
using Api.Dtos.MovieSeries;
using Api.Data;
using AutoMapper;
using Api.Helper;
using Api.Dtos.Region;
using Api.Dtos.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Api.Helpers;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieSeriesController : Controller
    {
        private readonly IMovieSeriesRepository _movieSeriesRepository;
        private readonly IMovieEpisodeRepository _movieEpisodeRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly IYearRepository _yearRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MovieSeriesController(IMovieSeriesRepository movieSeriesRepository, IGenreRepository genreRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _movieSeriesRepository = movieSeriesRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var movieSeries = await _movieSeriesRepository.GetAll(query);
            _movieSeriesRepository.CalculateTotalView(movieSeries);
            
            var movieSeriesMap = _mapper.Map<List<MovieSeriesDto>>(movieSeries);         
            return Ok(movieSeriesMap);
        }      

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movieSeries = await _movieSeriesRepository.GetById(id);
            if (movieSeries == null)
            {
                return NotFound();
            }
            movieSeries.TotalNumberofViewers = movieSeries.Episodes.Sum(e => e.NumberofViewers);
            var movieSeriesMap = _mapper.Map<MovieSeriesDto>(movieSeries);

            return Ok(movieSeriesMap);
        }

        [HttpGet("name")]
        public async Task<IActionResult> GetByName(string name)
        {
            var movieSeries = await _movieSeriesRepository.GetByName(name);
            foreach (var series in movieSeries)
            {
                series.TotalNumberofViewers = series.Episodes.Sum(e => e.NumberofViewers);
            }
            var movieSeriesMap = _mapper.Map<List<MovieSeriesDto>>(movieSeries);

            return Ok(movieSeriesMap);

        }

       /* [HttpGet("MovieEpisode/{seriesId}")]
        public async Task<IActionResult> GetEpisodeBySeriesId(int seriesId)
        {
            var movieSeries = await _movieSeriesRepository.GetEpisodeBySeriesId(seriesId);
            if (movieSeries == null)
            {
                return NotFound();
            }
            
            var movieSeriesMap = _mapper.Map<MovieSeriesDto>(movieSeries);

            return Ok(movieSeriesMap);

        }*/
        
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateMovieSeriesDto movieSeriesDto)
        {         
            if (movieSeriesDto == null)
            {
                return BadRequest();
            }

            var duplicateMovieSeries = await _movieSeriesRepository.MovieSeriesExistsName(movieSeriesDto.Name);

            if (duplicateMovieSeries)
            {
                return BadRequest("Movie Series already exsists");
            }
            var movieSeriesMap = _mapper.Map<MovieSeries>(movieSeriesDto);

            movieSeriesMap.PosterPath = await _movieSeriesRepository.SaveImageAsync(movieSeriesDto.ImageFile);          

            await _movieSeriesRepository.CreateAsync(movieSeriesMap);
            
            return CreatedAtAction(nameof(GetById), new { id = movieSeriesMap.Id }, movieSeriesDto);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateMovieSeriesDto movieSeriesDto)
        {

            var existingSeries = await _movieSeriesRepository.MovieSeriesExists(id);
            if (!existingSeries)
            {
                return NotFound();
            }
            var series = await _movieSeriesRepository.GetById(id);
            var movieSeriesMap = _mapper.Map(movieSeriesDto, series);

            await _movieSeriesRepository.UpdateAsync(series);

            return Ok(movieSeriesMap);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movieSeries = await _movieSeriesRepository.GetById(id);
            await _movieSeriesRepository.DeleteAsync(movieSeries);
            return Ok("Delete successfully");
        }       
    }
}
