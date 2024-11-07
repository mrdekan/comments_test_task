using CommentsAPI.Data;
using CommentsAPI.Interfaces;
using CommentsAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommentsAPI.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CommentEntity?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<IEnumerable<CommentEntity>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task AddAsync(CommentEntity comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CommentEntity comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CommentEntity comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CommentEntity>> GetTopLayerComments(int count, int offset)
        {
            return await _context.Comments.Where(el => el.ParentId == null).ToListAsync();
        }

        //public async Task<int> CountChildren(CommentEntity comment)
        //{

        //}
    }
}
