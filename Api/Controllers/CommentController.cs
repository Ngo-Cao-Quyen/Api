using Api.Dtos.Comment;
using Api.Dtos.Genre;
using Api.Dtos.MovieSeries;
using Api.Extensions;
using Api.Helpers;
using Api.Interfaces;
using Api.Mappers;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMovieEpisodeRepository _movieEpisodeRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public CommentController(ICommentRepository commentRepository, IMapper mapper, UserManager<AppUser> userManager, IMovieEpisodeRepository movieEpisodeRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _userManager = userManager;
            _movieEpisodeRepository = movieEpisodeRepository;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var comment = await _commentRepository.GetAll(query);

            var commentMap = comment.Select(e => e.ToCommentDto());
            
            return Ok(commentMap);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var genre = await _commentRepository.GetById(id);
            if (genre == null)
            {
                return null;
            }
            var genreMap = _mapper.Map<GenreDto>(genre);

            return Ok(genreMap);
        }

        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var commentMap = _mapper.Map<Comment>(commentDto);
            commentMap.AppUserId = appUser.Id;
            

            await _commentRepository.CreateAsync(commentMap);

            return CreatedAtAction(nameof(GetById), new { id = commentMap.Id }, new {commentMap.Id, commentMap.Title, commentMap.Content, commentMap.AppUserId});
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCommentDto commentDto)
        {
            var existingComment = await _commentRepository.CommentExists(id);
            if (!existingComment)
            {
                return NotFound();
            }
            var comment = await _commentRepository.GetById(id);
            var commentMap = _mapper.Map(commentDto, comment);

            await _commentRepository.UpdateAsync(comment);

            return Ok(commentMap);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _commentRepository.GetById(id);
            await _commentRepository.DeleteAsync(genre);
            return Ok("Delete successfully");
        }

    }
}
