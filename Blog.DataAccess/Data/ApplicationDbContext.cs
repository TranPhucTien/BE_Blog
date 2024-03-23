using Blog.Models;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        removePrefix(modelBuilder);

        modelBuilder.Entity<Post>()
            .HasOne(p => p.Author)
            .WithMany()
            .HasForeignKey(p => p.AuthorId);

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
    
    public void removePrefix(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes ()) {
            var tableName = entityType.GetTableName();
            
            if (tableName != null && tableName.StartsWith("AspNet")) {
                entityType.SetTableName(tableName.Substring(6));
            }
        }
    }
}