using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    private List<Post> Posts { get; set; } = new List<Post>();
    public Task<Post> AddPostAsync(Post post)
    {
        post.ID = Posts.Any() ? Posts.Max(p => p.ID) + 1 : 1;
        Posts.Add(post);
        return Task.FromResult(post);
    }

    public Task UpdatePostAsync(Post post)
    {
        Post ? postToUpdate = Posts.FirstOrDefault(p => p.ID == post.ID);
        if (postToUpdate is null)
        {
            throw new InvalidOperationException($"Post {post.ID} does not exist");
        }

        Posts.Remove(postToUpdate);
        Posts.Add(post);
        return Task.CompletedTask;
    }

    public Task DeletePostAsync(int id)
    {
        Post? postToDelete = Posts.FirstOrDefault(p => p.ID == id);
        if (postToDelete is null)
        {
           throw new InvalidOperationException($"Post {id} does not exist"); 
        }
        Posts.Remove(postToDelete);
        return Task.CompletedTask;
    }

    public Task<Post> GetSinglePostAsync(int id)
    {
        Post? postToGet = Posts.FirstOrDefault(p => p.ID == id);
        if (postToGet is null)
        {
           throw new InvalidOperationException($"Post {id} does not exist");
        }
        return Task.FromResult(postToGet);
    }

    public IQueryable<Post> GetMultiplePosts()
    {
       return Posts.AsQueryable();
    }
}