using CommentsAPI.Data;
using CommentsAPI.Interfaces;
using CommentsAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CommentsAPI.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOptions;

        public CommentRepository(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
            _cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };
        }

        public async Task<CommentEntity?> GetByIdAsync(int id)
        {
            string cacheKey = $"Comment_{id}";
            if (_cache.TryGetValue(cacheKey, out CommentEntity cachedComment))
            {
                Console.WriteLine("Cache was used");
                return cachedComment;
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _cache.Set(cacheKey, comment, _cacheOptions);
            }

            return comment;
        }

        public async Task<IEnumerable<CommentEntity>> GetAllAsync()
        {
            var comments = await _context.Comments.ToListAsync();
            return comments;
        }

        public async Task AddAsync(CommentEntity comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            _cache.Remove($"Children_{comment.ParentId}");
            _cache.Set($"Сomment_{comment.Id}", comment);
        }

        public async Task UpdateAsync(CommentEntity comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
            string cacheKey = $"Comment_{comment.Id}";
            _cache.Set(cacheKey, comment, _cacheOptions);
            _cache.Remove($"Children_{comment.ParentId}");
        }

        public async Task DeleteAsync(CommentEntity comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            string cacheKey = $"Comment_{comment.Id}";
            _cache.Remove(cacheKey);
        }

        public async Task<(IEnumerable<CommentEntity> Comments, int TotalPages)> GetTopLayerComments(int count, int offset)
        {
            int totalComments = await _context.Comments
        .Where(el => el.ParentId == null)
        .CountAsync();
            var comments = await _context.Comments
                .Where(el => el.ParentId == null)
                .OrderByDescending(c => c.CreatedAt)
                .Include(c => c.Comments)
                .Skip((offset - 1) * count)
                .Take(count)
                .ToListAsync();

            int totalPages = (int)Math.Ceiling((double)totalComments / count);

            return (comments, totalPages);
        }

        public async Task<IEnumerable<CommentEntity>> GetChildrenAsync(int parentId)
        {
            string cacheKey = $"Children_{parentId}";
            if (_cache.TryGetValue(cacheKey, out List<CommentEntity> cachedChildren))
            {
                Console.WriteLine("Cache was used");
                return cachedChildren;
            }

            var children = await _context.Comments
                .Where(c => c.ParentId == parentId)
                .OrderByDescending(c => c.CreatedAt)
                .Include(c => c.Comments)
                .ToListAsync();

            _cache.Set(cacheKey, children, _cacheOptions);
            return children;
        }
    }
}
