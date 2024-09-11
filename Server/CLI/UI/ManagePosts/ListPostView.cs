using Entities;
using InMemoryRepositories;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostView
{
    private ViewHandler viewHandler;
    private readonly IPostRepository postRepository;

    public ListPostView(IPostRepository postRepository, ViewHandler viewHandler)
    {
        this.postRepository = postRepository;
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
            Console.WriteLine("\n====================\n");
        }

        Console.WriteLine("Choose a post by typing the ID:");
        string? input = Console.ReadLine();
        int inp = 0;
        try
        {
            inp = int.Parse(input);
        }
        catch (FormatException e)
        {
            Console.WriteLine("Please enter an integer.");
        }
        foreach (var post in posts)
        {
            if (post.ID == inp)
            {
                viewHandler.ShowPost(post.ID);
            }
            else
            {
                Console.WriteLine($"Post {post.ID} does not exist.");
            }
        }
    }
}