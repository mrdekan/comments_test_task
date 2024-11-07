using CommentsAPI.Models.Entities;

namespace CommentsAPI.Interfaces
{
    public interface ICommentRepository
    {
        Task<CommentEntity?> GetByIdAsync(int id);
        Task<IEnumerable<CommentEntity>> GetAllAsync();
        Task<IEnumerable<CommentEntity>> GetTopLayerComments(int count, int offset);
        Task AddAsync(CommentEntity comment);
        Task UpdateAsync(CommentEntity comment);
        Task DeleteAsync(CommentEntity comment);
        Task<IEnumerable<CommentEntity>> GetChildrenAsync(int parentId);
    }
}
