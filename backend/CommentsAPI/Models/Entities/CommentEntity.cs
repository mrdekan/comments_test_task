using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommentsAPI.Models.Entities
{
    public class CommentEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? FileURL { get; set; }

        public string UserId { get; set; }

        public UserEntity User { get; set; }

        public string? ParentId { get; set; }

        public CommentEntity? Parent { get; set; }

        public ICollection<CommentEntity> Comments { get; set; }
    }
}
