using freddit.Data;
using freddit.Models;
using Microsoft.EntityFrameworkCore;

namespace freddit.Service;

public class DataService
{
    private readonly AppDbContext db;

    public DataService(AppDbContext db)
    {
        this.db = db;
    }

    /// <summary>
    /// Adds any missing example posts without duplicating existing seed posts.
    /// </summary>
    public void SeedData()
    {
        var posts = new List<Post>
        {
            new(new User("Anna"), "Velkommen til Freddit", "Hvad synes I om vores nye forum?", 5, 0),
            new(new User("Mikkel"), "Hvilket programmeringssprog bruger I mest?", "Jeg bruger C# til de fleste skoleprojekter.", 3, 1),
            new(new User("Sara"), "Tip til Entity Framework", "Husk at kalde SaveChangesAsync efter ændringer i databasen.", 8, 0),
            new(new User("Jonas"), "SQLite eller PostgreSQL?", "Hvilken database passer bedst til et lille skoleprojekt?", 2, 0),
            new(new User("Lea"), "Idéer til forsiden", "Hvilke funktioner vil I gerne se på Freddits forside?", 4, 1)
        };

        var seedTitles = posts.Select(post => post.Title).ToHashSet();
        var existingSeedTitles = db.Posts
            .Where(post => seedTitles.Contains(post.Title))
            .Select(post => post.Title)
            .ToHashSet();

        var missingPosts = posts
            .Where(post => !existingSeedTitles.Contains(post.Title))
            .ToList();

        if (missingPosts.Count == 0)
        {
            return;
        }

        db.Posts.AddRange(missingPosts);
        db.SaveChanges();
    }

    public List<Post> GetPosts()
    {
        return db.Posts
            .Include(post => post.User)
            .Include(post => post.Comments)
            .OrderByDescending(post => post.Id)
            .ToList();
    }

    public Post? GetPost(int id)
    {
        return db.Posts
            .Include(post => post.User)
            .Include(post => post.Comments)
            .FirstOrDefault(post => post.Id == id);
    }
}
