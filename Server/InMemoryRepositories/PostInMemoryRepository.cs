using DataTransferObjects;
using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    public List<Post> Posts { get; set; } = new List<Post>();

    public PostInMemoryRepository()
    {
        Post aboutDogs = new Post("About dogs",
            "Did you know that dogs have four legs thus making them quadropeds? Some dogs have double coats which helps them keep insulated; warm during the winter and cold during summers. Shaving this double coat can be quite problematic as it'll never grow back in the way it should. Only shave double coated dogs if you have no other option!",
            2);
        Post aCollectionOfPuns = new Post("Punderful punciyclopedia",
            "What's the difference between an arhaeologist and a grave raider?\nA diploma.\n\nWhat do you call a pile of cats?\nA meowntain.",
            4);
        AddPostAsync(aboutDogs);
        AddPostAsync(aCollectionOfPuns);
    }
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