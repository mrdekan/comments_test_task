using CommentsAPI.Models.Entities;

namespace CommentsAPI.Models.DTO
{
    public class CommentResponse
    {
        public CommentResponse(CommentEntity comment)
        {
            Id = comment.Id;
            Content = comment.Content;
            Author = new()
            {
                Username = comment.Username,
                Email = comment.Email,
                Homepage = comment.Homepage,
            };
            FileURL = comment.FileURL;
            ParentId = comment.ParentId;
            CreatedAt = comment.CreatedAt;
            ChildrenCount = comment.Comments == null ? 0 : comment.Comments.Count;
        }
        public int Id { get; set; }

        public int ChildrenCount { get; set; }

        public string Content { get; set; }

        public string? FileURL { get; set; }

        public int? ParentId { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public Author Author { get; set; }
    }
    public class Author
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string? Homepage { get; set; }
    }
}
