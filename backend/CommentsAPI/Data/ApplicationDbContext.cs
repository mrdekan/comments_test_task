using CommentsAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommentsAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CommentEntity> Comments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CommentEntity>()
                .HasOne(c => c.Parent)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
