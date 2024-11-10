namespace CommentsAPI.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file);
    }
}
