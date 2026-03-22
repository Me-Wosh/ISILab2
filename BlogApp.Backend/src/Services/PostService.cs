using BlogApp.Persistence;
using BlogApp.Persistence.Models;

namespace BlogApp.Services;

public record CreatePostRequest(string Title, string Content, int AuthorId);

public class PostService(BlogAppDbContext dbContext)
{
    public async Task<int> CreatePostAsync(CreatePostRequest request, CancellationToken cancellationToken)
    {
        var post = new Post
        {
            Title = request.Title,
            Content = request.Content,
            AuthorId = request.AuthorId
        };

        await dbContext.Posts.AddAsync(post, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return post.Id;
    }
}
