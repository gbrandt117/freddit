namespace freddit.Models;

public class Post
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Content { get; set; }
    
    public int Upvotes { get; set; }
    
    public int Downvotes { get; set; }
}