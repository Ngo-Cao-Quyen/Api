using Api.Dtos.Genre;
using Api.Dtos.Region;
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
        public async Task<IActionResult> GetAll()
        {
            var region = await _regionRepository.GetAll();
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
        public async Task<IActionResult> GetSeriesByRegion(int regionId)
        {
            var region = await _regionRepository.GetSeriesByRegion(regionId);
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

            var region = await _regionRepository.GetAll();
    
            var duplicateRegion = region.Any(e => e.Name.Trim().ToUpper() == regionDto.Name.Trim().ToUpper());

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
