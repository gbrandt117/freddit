using System;

namespace freddit.Models;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    
    public Comment(string content = "", int upvotes = 0, int downvotes = 0)
    {
        Content = content;
        Upvotes = upvotes;
        Downvotes = downvotes;
    }
    public Comment() {
        Id = 0;
        Content = "";
        Upvotes = 0;
        Downvotes = 0;
    }
}