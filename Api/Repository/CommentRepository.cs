using Api.Data;
using Api.Helpers;
using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MovieDbContext _context;
        public CommentRepository(MovieDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>>GetAll(QueryObject query)
        {
            return await _context.Comment.Include(e => e.AppUser).ToListAsync();
        }

        public async Task<Comment>GetById(int id)
        {
            var comment = await _context.Comment.FindAsync(id);
            return comment;
        }

        public async Task<Comment>CreateAsync(Comment comment)
        {
            await _context.Comment.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment>UpdateAsync(Comment comment)
        {        
            _context.Comment.Update(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment>DeleteAsync(Comment comment)
        {
           _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> CommentExists(int id)
        {
            return await _context.Comment.AnyAsync(e => e.Id == id);    
        }

    }
}
