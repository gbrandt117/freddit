using System;

namespace freddit.Models;

public class Comment
{
    
    public int Id { get; set; }
    
    public string Content { get; set; } = string.Empty; 
    
    public string Author { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; }
    
    public int Upvotes { get; set; }
    
    public int Downvotes { get; set; }

    public Comment
    {
        Upvotes = Upvotes;
        Downvotes = Downvotes;
        


    }

  

}