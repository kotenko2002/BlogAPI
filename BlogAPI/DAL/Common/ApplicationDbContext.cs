using BlogAPI.DAL.Entities.Categories;
using BlogAPI.DAL.Entities.Comments;
using BlogAPI.DAL.Entities.Hashtags;
using BlogAPI.DAL.Entities.Posts;
using BlogAPI.DAL.Entities.PostsHashtags;
using BlogAPI.DAL.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.DAL.Common
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostHashtag> PostsHashtags { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.Property(u => u.FirstName).IsRequired();
                builder.Property(u => u.LastName).IsRequired();
            });

            modelBuilder.Entity<Post>(builder =>
            {
                builder.Property(u => u.Title).IsRequired();
                builder.Property(u => u.Description).IsRequired();
                builder.Property(u => u.PhotoFileName).IsRequired();
                builder.Property(u => u.CreatedAt).IsRequired();

                builder
                   .HasOne(u => u.Author)
                   .WithMany()
                   .HasForeignKey(u => u.AuthorId);

                builder
                   .HasOne(c => c.Category)
                   .WithMany(p => p.Posts)
                   .HasForeignKey(c => c.CategoryId);
            });

            modelBuilder.Entity<PostHashtag>(builder =>
            {
                builder
                   .HasKey(ph => new { ph.PostId, ph.HashtagId });

                builder
                    .HasOne(ph => ph.Post)
                    .WithMany(p => p.PostHashtags)
                    .HasForeignKey(ph => ph.PostId);

                builder
                    .HasOne(ph => ph.Hashtag)
                    .WithMany(h => h.PostHashtags)
                    .HasForeignKey(ph => ph.HashtagId);
            });

            modelBuilder.Entity<Comment>(builder =>
            {
                builder
                   .HasOne(c => c.Author)
                   .WithMany()
                   .HasForeignKey(c => c.AuthorId)
                   .OnDelete(DeleteBehavior.Restrict);

                builder
                   .HasOne(c => c.Post)
                   .WithMany(p => p.Comments)
                   .HasForeignKey(c => c.PostId)
                   .OnDelete(DeleteBehavior.Cascade);

                builder
                   .HasOne(c => c.ParentComment)
                   .WithMany(c => c.Replies)
                   .HasForeignKey(c => c.ParentCommentId)
                   .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
