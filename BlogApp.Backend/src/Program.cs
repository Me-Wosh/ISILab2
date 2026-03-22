using BlogApp.Persistence;
using BlogApp.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BlogAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogAppDb"));
});

builder.Services.AddScoped<PostService>();

var app = builder.Build();

var group = app.MapGroup("/api")
    .DisableAntiforgery();

group.MapPost("/posts", async Task<Created<int>> (
    [FromForm] CreatePostRequest request,
    PostService postService,
    CancellationToken cancellationToken) =>
{
    var id = await postService.CreatePostAsync(request, cancellationToken);
    return TypedResults.Created($"/api/posts/{id}", id);
});

app.Run();
