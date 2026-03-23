using BlogApp.Persistence;
using BlogApp.Persistence.Models;
using BlogApp.Services;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.IntegrationTests;

public class PostServiceTests
{
    private readonly BlogAppDbContext _dbContext;

    public PostServiceTests()
    {
        var options = new DbContextOptionsBuilder<BlogAppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestBlogAppDb")
            .Options;

        _dbContext = new BlogAppDbContext(options);
        _dbContext.Database.EnsureDeleted();
        SeedData();
    }

    [Fact]
    public async Task GetAllPostsAsync_Returns3Posts()
    {
        // Arrange
        var service = new PostService(_dbContext);

        // Act
        var posts = await service.GetAllPostsAsync(CancellationToken.None);

        // Assert
        Assert.NotEmpty(posts);
        Assert.Equal(3, posts.Count());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetPostByIdAsync_ReturnsPost_GivenExistingId(int id)
    {
        // Arrange
        var service = new PostService(_dbContext);

        // Act
        var post = await service.GetPostByIdAsync(id, CancellationToken.None);

        // Assert
        Assert.NotNull(post);
        Assert.Equal(id, post.Id);
    }

    private void SeedData()
    {
        var posts = new List<Post>
        {
            new() { Id = 1, Title = "Post 1", Content = "Post 1 content", AuthorId = 1 },
            new() { Id = 2, Title = "Post 2", Content = "Post 2 content", AuthorId = 1 },
            new() { Id = 3, Title = "Post 3", Content = "Post 3 content", AuthorId = 1 }
        };

        _dbContext.Posts.AddRange(posts);
        _dbContext.SaveChanges();
    }
}
