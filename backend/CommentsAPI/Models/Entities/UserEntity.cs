using Microsoft.AspNetCore.Identity;

namespace CommentsAPI.Models.Entities
{
    public class UserEntity : IdentityUser
    {
        public string AvatarURL { get; set; }

        public ICollection<CommentEntity> Comments { get; set; }
    }
}
