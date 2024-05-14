using Api.Dtos.MovieEpisode;
using Api.Dtos.MovieSeries;
using Api.Helpers;
using Api.Interfaces;
using Api.Models;
using Api.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieEpisodeController : Controller
    {
        private readonly IMovieEpisodeRepository _movieEpisodeRepository;
        private readonly IMapper _mapper;
        public MovieEpisodeController(IMovieEpisodeRepository movieEpisodeRepository, IMapper mapper)
        {
            _movieEpisodeRepository = movieEpisodeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var movieEpisode = await _movieEpisodeRepository.GetAll(query);
            var movieEpisodeMap = _mapper.Map<List<MovieEpisodeDto>>(movieEpisode);

            return Ok(movieEpisodeMap);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movieEpisode = await _movieEpisodeRepository.GetById(id);
            if (movieEpisode == null)
            {
                return NotFound();
            }
            await _movieEpisodeRepository.UpdateViewCountAsync(id);
            var movieEpisodeMap = _mapper.Map<MovieEpisodeDto>(movieEpisode);

            return Ok(movieEpisodeMap);
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(CreateMovieEpisodeDto movieEpisodeDto)
        {
           
            var movieEpisodeMap = _mapper.Map<MovieEpisode>(movieEpisodeDto);
            await _movieEpisodeRepository.CreateAsync(movieEpisodeMap);
      

            return CreatedAtAction(nameof(GetById), new { id = movieEpisodeMap.Id }, movieEpisodeDto);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MovieEpisodeDto movieEpisodeDto)
        {
            var existingEpisode = await _movieEpisodeRepository.MovieEpisodeExists(id);
            if(!existingEpisode)
            {
                return NotFound();
            }
            var episode = await _movieEpisodeRepository.GetById(id);
            var movieEpisodeMap = _mapper.Map(movieEpisodeDto, episode);
            await _movieEpisodeRepository.UpdateAsync(episode);

            return Ok(movieEpisodeMap);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var episode = await _movieEpisodeRepository.GetById(id);
            
            await _movieEpisodeRepository.DeleteAsync(episode);

            return Ok("Delete successfully");
        }

       
    }
}
