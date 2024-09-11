using Entities;
using InMemoryRepositories;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostView
{
    private ViewHandler viewHandler;

    private readonly IPostRepository postRepository =
        new PostInMemoryRepository();

    public ListPostView(ViewHandler viewHandler)
    {
        this.viewHandler = viewHandler;
    }

    public void Show()
    {
        Console.WriteLine("Available Posts:");
        var posts = postRepository.GetMultiplePosts().ToList();

        if (!posts.Any())
        {
            Console.WriteLine("No posts available.");
            return;
        }
        
        foreach (var post in posts)
        {
            Console.WriteLine($"Post ID: {post.ID}");
            Console.WriteLine($"Title: {post.Title}");
            Console.WriteLine($"Body: {post.Content}\n");
            Console.WriteLine(
                "\n====================\n"); 
        }

        Console.WriteLine("Press any key to return to the menu.");
        Console.ReadKey();
    }
}