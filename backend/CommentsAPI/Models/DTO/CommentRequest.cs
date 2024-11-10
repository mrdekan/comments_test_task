using System.ComponentModel.DataAnnotations;

namespace CommentsAPI.Models.DTO
{
    public class CommentRequest
    {
        [MaxLength(1000)]
        public string Content { get; set; }

        [MaxLength(50)]
        public string Username { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string? Homepage { get; set; }

        public IFormFile? File { get; set; }

        public int? ParentId { get; set; }

        public string CaptchaText { get; set; }
    }
}
