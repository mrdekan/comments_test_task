using CommentsAPI.Models.Entities;

namespace CommentsAPI.Interfaces
{
    public interface ICommentRepository
    {
        Task<CommentEntity?> GetByIdAsync(int id);
        Task<IEnumerable<CommentEntity>> GetAllAsync();
        Task<(IEnumerable<CommentEntity> Comments, int TotalPages)> GetTopLayerComments(int count, int offset, string? sort);
        Task AddAsync(CommentEntity comment);
        Task UpdateAsync(CommentEntity comment);
        Task DeleteAsync(CommentEntity comment);
        Task<IEnumerable<CommentEntity>> GetChildrenAsync(int parentId);
    }
}
