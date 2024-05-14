using Api.Extensions;
using Api.Interfaces;
using Api.Models;
using Api.Dtos.Reaction;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactionController : Controller
    {
        private readonly IReactionRepository _reactionRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public ReactionController(IReactionRepository reactionRepository, UserManager<AppUser> userManager, IMapper mapper)
        {
            _reactionRepository = reactionRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost("like")]
        public async Task<IActionResult> Like([FromBody] ReactionDto reactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var map = _mapper.Map<Reaction>(reactionDto);
                await _reactionRepository.LikeAsync(map);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("unlike")]
        public async Task<IActionResult> Unlike([FromBody] ReactionDto reactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var map = _mapper.Map<Reaction>(reactionDto);
                await _reactionRepository.UnlikeAsync(map);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
