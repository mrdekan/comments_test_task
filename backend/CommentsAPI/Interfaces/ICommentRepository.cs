using CommentsAPI.Models.Entities;

namespace CommentsAPI.Interfaces
{
    public interface ICommentRepository
    {
        Task<CommentEntity?> GetByIdAsync(int id);
        Task<IEnumerable<CommentEntity>> GetAllAsync();
        Task AddAsync(CommentEntity comment);
        Task UpdateAsync(CommentEntity comment);
        Task DeleteAsync(CommentEntity comment);
    }
}
