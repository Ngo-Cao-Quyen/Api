using Api.Dtos.Region;
using Api.Dtos.Year;
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
    public class YearController : Controller
    {
        private readonly IYearRepository _yearRepository;
        private readonly IMapper _mapper;
        public YearController(IYearRepository yearRepository, IMapper mapper)
        {
            _yearRepository = yearRepository;
            _mapper = mapper;
        }

        [HttpGet]
       
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var year = await _yearRepository.GetAll(query);
            var yearMap = _mapper.Map<List<YearDto>>(year);

            return Ok(yearMap);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var year = await _yearRepository.GetById(id);
            if (year == null)
            {
                return null;
            }
            var yearMap = _mapper.Map<YearDto>(year);

            return Ok(yearMap);
        }

        [HttpGet("MovieSeries/{yearId}")]
        public async Task<IActionResult> GetSeriesByYear(int yearId, [FromQuery] QueryObject query)
        {
            var year = await _yearRepository.GetSeriesByYear(yearId, query);
            if (year == null)
            {
                return NotFound();
            }

            var yearMap = _mapper.Map<YearDto>(year);

            return Ok(yearMap);
        }

        [HttpPost]
        
        public async Task<IActionResult> Create(CreateYearDto yearDto)
        {
            if (yearDto == null)
            {
                return BadRequest();
            }

            var duplicateYear = await _yearRepository.YearExistsName(yearDto.Name);

            if (duplicateYear)
            {
                return BadRequest("Year already exists");
            }
            var yearMap = _mapper.Map<Year>(yearDto);

            await _yearRepository.CreateAsync(yearMap);

            return CreatedAtAction(nameof(GetById), new { id = yearMap.Id }, yearMap);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, YearDto yearDto)
        {
            var existingYear = await _yearRepository.YearExists(id);
            if (!existingYear)
            {
                return NotFound();
            }
            var year = await _yearRepository.GetById(id);
            var yearMap = _mapper.Map(yearDto, year);
            await _yearRepository.UpdateAsync(year);
            return Ok(yearMap);
        }
    }
}
