namespace CommentsAPI.Models.DTO
{
    public class CommentRequest
    {
        public string Content { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string? Homepage { get; set; }

        public IFormFile? File { get; set; }

        public int? ParentId { get; set; }

        public string CaptchaText { get; set; }
    }
}
