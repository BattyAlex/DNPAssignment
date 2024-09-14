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
        Console.WriteLine("[1 - View post]\n[2 - Back]");
        string? input = Console.ReadLine();
        int choosenAction = 0;
        while (choosenAction == 0)
        {
            try
            {
                choosenAction = int.Parse(input);
            }
            catch (Exception e)
            {
                Console.WriteLine("[1 - View post]\n[2 - Back]");
                input = Console.ReadLine();
            }
        }
        if (choosenAction == 1)
        {
            postChosen(posts);
        }
        else if (choosenAction == 2)
        {
           viewHandler.ChangeView(ViewHandler.MANAGEPOST); 
        }
        else
        {
            Console.WriteLine("Goodbye!");
        }
    }

    private void postChosen(List<Post> posts)
    {
        Console.WriteLine("Choose a post by typing the ID:");
        string? input = Console.ReadLine();
        int inp = 0;
        while (inp == 0)
        { 
            try
            {
                inp = int.Parse(input);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Choose a post by typing the ID:");
                input = Console.ReadLine();
            }
        }
        bool postExists = false;
        foreach (var post in posts)
        {
            if (post.ID == inp)
            {
                viewHandler.ShowPost(post.ID);
                postExists = true;
            }
        }
        if (!postExists)
        {
            Console.WriteLine($"Post {inp} does not exist.");
        }
    }
}