using Blog.Models;
using Blog.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Post>()
            .HasOne(p => p.Author)
            .WithMany()
            .HasForeignKey(p => p.AuthorId);
        
        // modelBuilder.Entity<Post>()
        //     .Property(e => e.Id)
        //     .UseMySqlIdentityColumn();

        
        // modelBuilder.Entity<User>().HasData(
        //     new User
        //     {
        //         Id = 1,
        //         Username = "tientp",
        //         Email = "tranphuctien2003nd@gmail.com",
        //         Password = "123456"
        //     });

        // modelBuilder.Entity<Tag>().HasData(
        //     new Tag
        //     {
        //         Name = "Lập trình"
        //     },
        //     new Tag
        //     {
        //         Name = "Công nghệ"
        //     },
        //     new Tag
        //     {
        //         Name = "OOP"
        //     }
        // );

        // modelBuilder.Entity<PostTag>().HasData(
        //     new PostTag
        //     {
        //         PostId = 1,
        //         TagId = 1
        //     },
        //     new PostTag
        //     {
        //         PostId = 1,
        //         TagId = 2
        //     }
        // );
    }
}