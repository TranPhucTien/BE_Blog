using Blog.Models.Entities;
using Microsoft.AspNetCore.Identity;
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
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<PostTag> PostTags { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<BookMark> Bookmarks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        RemovePrefix(modelBuilder);

        modelBuilder.Entity<Post>()
            .HasOne(p => p.Author)
            .WithMany()
            .HasForeignKey(p => p.AuthorId);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId);
        
        modelBuilder.Entity<PostTag>().HasKey(pt => new { pt.PostId, pt.TagId });
        
        modelBuilder.Entity<PostTag>()
            .HasOne(pt => pt.Post)
            .WithMany(p => p.PostTags)
            .HasForeignKey(pt => pt.PostId);
        
        modelBuilder.Entity<PostTag>()
            .HasOne(pt => pt.Tag)
            .WithMany(t => t.PostTags)
            .HasForeignKey(pt => pt.TagId);



        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER"
            }
        };
        
        modelBuilder.Entity<IdentityRole>().HasData(roles);

        modelBuilder.Entity<Tag>().HasData(
            new Tag
            {
                Id = 1,
                Name = "Lập trình"
            },
            new Tag
            {
                Id = 2,
                Name = "Công nghệ"
            },
            new Tag
            {
                Id = 3,
                Name = "OOP"
            },
            new Tag
            {
                Id = 4,
                Name = "Quản lý dự án"
            }
        );
    }
    
    public void RemovePrefix(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes ()) {
            var tableName = entityType.GetTableName();
            
            if (tableName != null && tableName.StartsWith("AspNet")) {
                entityType.SetTableName(tableName.Substring(6));
            }
        }
    }
}