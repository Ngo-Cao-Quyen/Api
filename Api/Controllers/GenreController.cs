using Api.Dtos.Genre;
using Api.Dtos.MovieEpisode;
using Api.Dtos.MovieSeries;
using Api.Interfaces;
using Api.Models;
using Api.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : Controller
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMovieSeriesRepository _movieSeriesRepository;
        private readonly IMapper _mapper;

        public GenreController(IGenreRepository genreRepository, IMovieSeriesRepository movieSeriesRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _movieSeriesRepository = movieSeriesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult>GetAll()
        { 
            var genre = await _genreRepository.GetAll();
           
            var genreMap = _mapper.Map<List<GenreDto>>(genre);    

            return Ok(genreMap);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var genre = await _genreRepository.GetById(id);
            if (genre == null)
            {
                return null;
            }
            var genreMap = _mapper.Map<GenreDto>(genre);

            return Ok(genreMap);           
        }

        [HttpGet("movieSeries/{genreId}")]
        public async Task<IActionResult> GetMovieSeriesByGenre(int genreId)
        {
            var movieSeries = await _genreRepository.GetMovieSeriesByGenre(genreId);

            var movieSeriesMap = _mapper.Map<List<MovieSeriesDto>>(movieSeries);

            return Ok(movieSeriesMap);
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(CreateGenreDto genreDto)
        {
            if(genreDto == null)
            {
                return BadRequest();
            }
            var genre = await _genreRepository.GetAll();
            var dulicateGenre = genre.Any(e => e.Name.Trim().ToUpper() ==  genreDto.Name.Trim().ToUpper());
            if (dulicateGenre)
            {
                return BadRequest("Genre already exists");
            }
            var genreMap = _mapper.Map<Genre>(genreDto);
            await _genreRepository.CreateAsync(genreMap);

            return CreatedAtAction(nameof(GetById), new {id = genreMap.Id}, genreMap);          
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateGenreDto genreDto)
        {
            var existingGenre = await _genreRepository.GenreExists(id);
            if (!existingGenre)
            {
                return NotFound();
            }
            var genre = await _genreRepository.GetById(id);
            var genreMap = _mapper.Map(genreDto, genre);

            await _genreRepository.UpdateAsync(genre);

            return Ok(genreMap);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _genreRepository.GetById(id);
            await _genreRepository.DeleteAsync(genre);
            return Ok("Delete successfully");
        }
    }
}
