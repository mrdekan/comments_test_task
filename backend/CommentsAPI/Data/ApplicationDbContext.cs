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

            modelBuilder.Entity<CommentEntity>().HasData(
                new CommentEntity { Id = 11101, Content = "This is the first comment!", CreatedAt = DateTime.Now.AddMinutes(-10), Username = "user1", Email = "user1@example.com", Homepage = "https://user1.com", ParentId = null, FileURL = "gt86.png" },
                new CommentEntity { Id = 11102, Content = "I agree with the above comment.", CreatedAt = DateTime.Now.AddMinutes(-8), Username = "user2", Email = "user2@example.com", ParentId = 11107, FileURL = "hello.txt" },
                new CommentEntity { Id = 11103, Content = "This is a great post. Thank you for sharing!", CreatedAt = DateTime.Now.AddHours(-6), Username = "user3", Email = "user3@example.com", ParentId = null, FileURL = "code.txt" },
                new CommentEntity { Id = 11104, Content = "I have a question about the code in the previous post.", CreatedAt = DateTime.Now.AddMinutes(-5), Username = "user4", Email = "user4@example.com", ParentId = 11107, FileURL = null },
                new CommentEntity { Id = 11105, Content = "<strong>This is a bold comment.</strong>", CreatedAt = DateTime.Now.AddMinutes(-4), Username = "user5", Email = "user5@example.com", ParentId = null, FileURL = "tiger.jpg" },
                new CommentEntity { Id = 11106, Content = "Can you provide more details on this?", CreatedAt = DateTime.Now.AddMinutes(-3), Username = "user6", Email = "user6@example.com", ParentId = 11111, FileURL = "test.gif" },
                new CommentEntity { Id = 11107, Content = "<i>This comment is in italics.</i>", CreatedAt = DateTime.Now.AddDays(-2), Username = "user7", Email = "user7@example.com", ParentId = null, FileURL = null },
                new CommentEntity { Id = 11108, Content = "I found the issue. It was a small typo.", CreatedAt = DateTime.Now.AddMinutes(-1), Username = "user8", Email = "user8@example.com", ParentId = 11109, FileURL = "hello.txt" },
                new CommentEntity { Id = 11109, Content = "Great job on the tutorial!", CreatedAt = DateTime.Now, Username = "user9", Email = "user9@example.com", ParentId = null, FileURL = "gt86.png" },
                new CommentEntity { Id = 11110, Content = "Thank you for your feedback!", CreatedAt = DateTime.Now.AddDays(-2), Username = "user10", Email = "user10@example.com", ParentId = 11130, FileURL = null },
                new CommentEntity { Id = 11111, Content = "I have a suggestion. Maybe you could add more examples.", CreatedAt = DateTime.Now.AddDays(-4), Username = "user11", Email = "user11@example.com", ParentId = null, FileURL = "code.txt" },
                new CommentEntity { Id = 11112, Content = "Could you explain how to use this in production?", CreatedAt = DateTime.Now.AddMinutes(-5), Username = "user12", Email = "user12@example.com", ParentId = 11101, FileURL = "test.gif" },
                new CommentEntity { Id = 11113, Content = "This is a helpful post. I learned a lot.", CreatedAt = DateTime.Now.AddMinutes(-7), Username = "user13", Email = "user13@example.com", ParentId = null, FileURL = "tiger.jpg" },
                new CommentEntity { Id = 11114, Content = "<code>let x = 10;</code> Simple code example.", CreatedAt = DateTime.Now.AddMinutes(-8), Username = "user14", Email = "user14@example.com", ParentId = 11112, FileURL = null },
                new CommentEntity { Id = 11115, Content = "Nice! I will try this approach.", CreatedAt = DateTime.Now.AddHours(-10), Username = "user15", Email = "user15@example.com", ParentId = null, FileURL = "gt86.png" },
                new CommentEntity { Id = 11116, Content = "Is there any way to optimize this further?", CreatedAt = DateTime.Now.AddHours(-12), Username = "user16", Email = "user16@example.com", ParentId = 11112, FileURL = "hello.txt" },
                new CommentEntity { Id = 11117, Content = "I would love to see a video tutorial on this.", CreatedAt = DateTime.Now.AddMinutes(-14), Username = "user17", Email = "user17@example.com", ParentId = null, FileURL = "code.txt" },
                new CommentEntity { Id = 11118, Content = "I'm stuck. Can someone help me with this part?", CreatedAt = DateTime.Now.AddMinutes(-15), Username = "user18", Email = "user18@example.com", ParentId = 11111, FileURL = null },
                new CommentEntity { Id = 11119, Content = "<strong>Very informative!</strong> I will be using this soon.", CreatedAt = DateTime.Now.AddMinutes(-18), Username = "user19", Email = "user19@example.com", ParentId = null, FileURL = "test.gif" },
                new CommentEntity { Id = 11120, Content = "Can you provide the source code for this?", CreatedAt = DateTime.Now.AddMinutes(-20), Username = "user20", Email = "user20@example.com", ParentId = 11110, FileURL = "tiger.jpg" },
                new CommentEntity { Id = 11121, Content = "I think there's an error in this example.", CreatedAt = DateTime.Now.AddMinutes(-22), Username = "user21", Email = "user21@example.com", ParentId = null, FileURL = "code.txt" },
                new CommentEntity { Id = 11122, Content = "Can you explain the error you're facing?", CreatedAt = DateTime.Now.AddMinutes(-24), Username = "user22", Email = "user22@example.com", ParentId = 11101, FileURL = null },
                new CommentEntity { Id = 11123, Content = "I'm having trouble understanding this part.", CreatedAt = DateTime.Now.AddMinutes(-26), Username = "user23", Email = "user23@example.com", ParentId = null, FileURL = "hello.txt" },
                new CommentEntity { Id = 11124, Content = "Can someone clarify how to use the function?", CreatedAt = DateTime.Now.AddMinutes(-28), Username = "user24", Email = "user24@example.com", ParentId = 11125, FileURL = "gt86.png" },
                new CommentEntity { Id = 11125, Content = "I love how simple this approach is.", CreatedAt = DateTime.Now.AddMinutes(-30), Username = "user25", Email = "user25@example.com", ParentId = null, FileURL = "code.txt" },
                new CommentEntity { Id = 11126, Content = "This helped me a lot. Thanks!", CreatedAt = DateTime.Now.AddMinutes(-32), Username = "user26", Email = "user26@example.com", ParentId = 11126, FileURL = null },
                new CommentEntity { Id = 11127, Content = "Is it possible to use this with a different framework?", CreatedAt = DateTime.Now.AddMinutes(-34), Username = "user27", Email = "user27@example.com", ParentId = null, FileURL = "test.gif" },
                new CommentEntity { Id = 11128, Content = "Thanks for the clarification.", CreatedAt = DateTime.Now.AddMinutes(-36), Username = "user28", Email = "user28@example.com", ParentId = 11125, FileURL = "hello.txt" },
                new CommentEntity { Id = 11129, Content = "<code>const y = 20;</code> Another code example.", CreatedAt = DateTime.Now.AddMinutes(-38), Username = "user29", Email = "user29@example.com", ParentId = null, FileURL = "tiger.jpg" },
                new CommentEntity { Id = 11130, Content = "This is great! I'm going to try it.", CreatedAt = DateTime.Now.AddMinutes(-40), Username = "user30", Email = "user30@example.com", ParentId = 11126, FileURL = "code.txt" }
            );
        }
    }
}
