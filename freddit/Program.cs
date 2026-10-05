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

app.MapPost("/api/posts", async (Post post, AppDbContext db) =>
{
    db.Posts.Add(post);
    await db.SaveChangesAsync();

    return Results.Created($"/api/posts/{post.Id}", post);
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
app.MapPut("/api/posts/{id}/upvote",() =>
{

});

app.MapPut("/api/posts/{id}/downvote",() =>
{

});

app.MapPut("/api/posts/{postid}/comments/{commentid}/upvote",() =>
{

});

app.MapPut("/api/posts/{postid}/comments/{commentid}/downvote",() =>
{

});


//POST

app.MapPost("/api/posts/{id}/comments",() =>
{

});

app.Run();

