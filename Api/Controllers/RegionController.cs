using Api.Dtos.Genre;
using Api.Dtos.Region;
using Api.Helpers;
using Api.Interfaces;
using Api.Models;
using Api.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : Controller
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var region = await _regionRepository.GetAll(query);
            var regionMap = _mapper.Map<List<RegionDto>>(region);

            return Ok(regionMap);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var region = await _regionRepository.GetById(id);
            if (region == null)
            {
                return NotFound();
            }

            var regionMap = _mapper.Map<RegionDto>(region) ;

            return Ok(regionMap);
        }

        [HttpGet("MovieSeries/{regionId}")]
        public async Task<IActionResult> GetSeriesByRegion(int regionId, [FromQuery] QueryObject query)
        {
            var region = await _regionRepository.GetSeriesByRegion(regionId, query);
            if (region == null)
            {
                return NotFound();
            }

            var regionMap = _mapper.Map<RegionDto>(region);

            return Ok(regionMap);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRegionDto regionDto)
        {
            if (regionDto == null)
            {
                return BadRequest();
            }
    
            var duplicateRegion = await _regionRepository.RegionExistsName(regionDto.Name);

            if (duplicateRegion)
            {
                return BadRequest("Region already exsists");
            }
            var regionMap = _mapper.Map<Region>(regionDto);

            await _regionRepository.CreateAsync(regionMap);
         
            return CreatedAtAction(nameof(GetById), new { id = regionMap.Id }, regionMap);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateRegionDto regionDto)
        {
            var existingRegion = await _regionRepository.RegionExists(id);
            if(!existingRegion)
            {
                return NotFound();
            }
            var region = await _regionRepository.GetById(id);
            var regionMap = _mapper.Map(regionDto, region);
            await _regionRepository.UpdateAsync(region);
            return Ok(regionMap);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var region = await _regionRepository.GetById(id);
            await _regionRepository.DeleteAsync(region);
            return Ok("Delete successfully");
        }

    }
}
