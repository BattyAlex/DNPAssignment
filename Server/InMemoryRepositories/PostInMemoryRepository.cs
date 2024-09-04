using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    private List<Post> posts { get; set; } = new List<Post>();
    public Task<Post> AddPostAsync(Post post)
    {
        post.ID = posts.Any() ? posts.Max(p => p.ID) + 1 : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task UpdatePostAsync(Post post)
    {
        Post ? postToUpdate = posts.FirstOrDefault(p => p.ID == post.ID);
        if (postToUpdate is null)
        {
            throw new InvalidOperationException($"Post {post.ID} does not exist");
        }

        posts.Remove(postToUpdate);
        posts.Add(post);
        return Task.CompletedTask;
    }

    public Task DeletePostAsync(int ID)
    {
        Post? postToDelete = posts.FirstOrDefault(p => p.ID == ID);
        if (postToDelete is null)
        {
           throw new InvalidOperationException($"Post {ID} does not exist"); 
        }
        posts.Remove(postToDelete);
        return Task.CompletedTask;
    }

    public Task<Post> GetSinglePostAsync(int ID)
    {
        Post? postToGet = posts.FirstOrDefault(p => p.ID == ID);
        if (postToGet is null)
        {
           throw new InvalidOperationException($"Post {ID} does not exist");
        }
        return Task.FromResult(postToGet);
    }

    public IQueryable<Post> GetMultiplePosts()
    {
       return posts.AsQueryable();
    }
}