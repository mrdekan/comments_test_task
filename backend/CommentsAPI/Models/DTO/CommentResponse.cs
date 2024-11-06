namespace CommentsAPI.Models.DTO
{
    public class CommentResponse
    {
        public int Id { get; set; }

        public int ChildrenCount { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string? Homepage { get; set; }

        public string? FileURL { get; set; }

        public int? ParentId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
