namespace freddit.Data;

using Microsoft.EntityFrameworkCore;
using freddit.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Post> Posts => Set<Post>();

    public DbSet<Comment> Comments => Set<Comment>();
    
    public DbSet<User> User => Set<User>();
}