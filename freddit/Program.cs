using Microsoft.EntityFrameworkCore;
using freddit.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors();

//GET
app.MapGet("/api/posts", () =>
{
    
});

app.MapGet("/api/posts/{id}",() =>
{

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

app.MapPost("/api/posts",() =>
{

});

app.MapPost("/api/posts/{id}/comments",() =>
{

});


app.Run();