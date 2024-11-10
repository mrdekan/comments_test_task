using CommentsAPI.Models.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommentsAPI.Models.Entities
{
    public class CommentEntity
    {
        public CommentEntity()
        {

        }
        public CommentEntity(CommentRequest comment, string? fileURL)
        {
            Content = comment.Content;
            CreatedAt = DateTime.Now;
            Username = comment.Username;
            Email = comment.Email;
            Homepage = comment.Homepage;
            ParentId = comment.ParentId;
            FileURL = fileURL;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string? Homepage { get; set; }

        public string? FileURL { get; set; }

        public int? ParentId { get; set; }

        public CommentEntity? Parent { get; set; }

        public ICollection<CommentEntity> Comments { get; set; }
    }
}
