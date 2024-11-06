using CommentsAPI.Models.Entities;

namespace CommentsAPI.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(UserEntity user);
        bool ValidateToken(string token);
    }
}
