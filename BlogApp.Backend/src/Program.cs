using BlogApp.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BlogAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogAppDb"));
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
