using Microsoft.EntityFrameworkCore;
using freddit.Data;
using freddit.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazor");


//GET
app.MapGet("/api/posts", async (AppDbContext db) =>
{
    return await db.Posts
        .Include(p => p.User)
        .Include(p => p.Comments)
        .ToListAsync();
});

app.MapGet("/api/posts/{id}", async (int id, AppDbContext db) =>
{
    var post = await db.Posts
        .Include(p => p.User)
        .Include(p => p.Comments)
        .FirstOrDefaultAsync(p => p.Id == id);

    if (post == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(post);
});

//put
app.MapPut("/api/posts/{id}/upvote", async (int id, AppDbContext db) =>
{
    var post = await db.Posts.FindAsync(id);

    if (post == null)
    {
        return Results.NotFound();
    }

    post.Upvotes++;

    await db.SaveChangesAsync();

    return Results.Ok(post);
});

app.MapPut("/api/posts/{id}/downvote", async (int id, AppDbContext db) =>
{
    var post = await db.Posts.FindAsync(id);

    if (post == null)
    {
        return Results.NotFound();
    }

    post.Downvotes--;

    await db.SaveChangesAsync();

    return Results.Ok(post);
});

app.MapPut("/api/posts/{postid}/comments/{commentid}/upvote",() =>
{

});

app.MapPut("/api/posts/{postid}/comments/{commentid}/downvote",() =>
{

});


//POST

app.MapPost("/api/posts", async (Post post, AppDbContext db) =>
{
    db.Posts.Add(post);
    await db.SaveChangesAsync();

    return Results.Created($"/api/posts/{post.Id}", post);
});

app.MapPost("/api/posts/{id}/comments", async (int id, Comment comment, AppDbContext db) =>
{
    var post = await db.Posts.FindAsync(id);

    if (post == null)
    {
        return Results.NotFound();
    }
    
    db.Comments.Add(comment);
    await db.SaveChangesAsync();

    return Results.Created(
        $"/api/posts/{id}/comments/{comment.Id}",
        comment
    );
});

app.Run();

