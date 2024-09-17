using System.Text.Json;
using Entities;
using RepositoryContracts;
namespace FileRepositories;

public class PostFileRepository: IPostRepository
{
    private readonly string filePath = "posts.json";

    public PostFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<Post> AddPostAsync(Post post)
    {
        List<Post> posts = LoadPosts().Result;
        int maxId = posts.Count > 0 ? posts.Max(x => x.ID) : 1;
        post.ID = maxId + 1;
        posts.Add(post);
        SavePosts(posts);
        return post;
    }

    public async Task UpdatePostAsync(Post post)
    {
        List<Post> posts = LoadPosts().Result;
        Post? postToUpdate = posts.FirstOrDefault(p => p.ID == post.ID);
        if (postToUpdate is null)
        {
            throw new InvalidOperationException($"Post {post.ID} does not exist");
        }
        posts.Remove(postToUpdate);
        posts.Add(post);
        SavePosts(posts);
    }

    public async Task DeletePostAsync(int id)
    {
        List<Post> posts = LoadPosts().Result;
        Post? postToDelete = posts.FirstOrDefault(p => p.ID == id);
        if (postToDelete is null)
        {
            throw new InvalidOperationException($"Post {id} does not exist"); 
        }
        posts.Remove(postToDelete);
        SavePosts(posts);
    }

    public async Task<Post> GetSinglePostAsync(int id)
    {
        List<Post> posts = LoadPosts().Result;
        Post? postToGet = posts.FirstOrDefault(p => p.ID == id);
        if (postToGet is null)
        {
            throw new InvalidOperationException($"Post {id} does not exist");
        }
        return postToGet;
    }

    public IQueryable<Post> GetMultiplePosts()
    {
        List<Post> posts = LoadPosts().Result;
        return posts.AsQueryable();
    }

    private async Task<List<Post>> LoadPosts()
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
        return posts;
    }

    private async void SavePosts(List<Post> posts)
    {
        string postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postsAsJson);
    }

}