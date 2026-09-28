namespace Models;

public class Post
{
    public int Id { get; set; }

    public string Title { get; set; } = "";

    public string? Text { get; set; }

    public string? Url { get; set; }

    public string AuthorName { get; set; } = "";

    public DateTime CreatedAt { get; set; }

    public int Upvotes { get; set; }

    public int Downvotes { get; set; }

    public List<Comment> Comments { get; set; } = new();
}