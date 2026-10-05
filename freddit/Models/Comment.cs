namespace freddit.Models;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }

    public int Upvotes { get; set; }
    public int Downvotes { get; set; }

    public User User { get; set; }
    public Post Post { get; set; }

    public Comment(
        string content = "",
        int upvotes = 0,
        int downvotes = 0)
    {
        Content = content;
        Upvotes = upvotes;
        Downvotes = downvotes;
        CreatedAt = DateTime.Now;
    }

    public Comment()
    {
        Id = 0;
        Content = "";
        CreatedAt = DateTime.Now;
        Upvotes = 0;
        Downvotes = 0;
    }
}