using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;
    private readonly ViewHandler viewHandler;
    private readonly UserLoggedIn userLoggedIn;

    public CreatePostView(IPostRepository postRepository, ViewHandler viewHandler, UserLoggedIn userLoggedIn)
    {
        this.postRepository = postRepository;
        this.viewHandler = viewHandler;
        this.userLoggedIn = userLoggedIn;
    }
    
    public async Task Start()
    {
        Console.WriteLine("Enter the title of the post:");
        string? title = Console.ReadLine();
        while (title is null)
        {
            Console.WriteLine("title is required");
            title = Console.ReadLine();
        }
        Console.WriteLine("Enter the content of the post:");
        string? content = Console.ReadLine();
        while (content is null)
        {
            Console.WriteLine("content is required");
            content = Console.ReadLine();
        }
        Post post = new Post(title, content, userLoggedIn.User.Id); 
        await postRepository.AddPostAsync(post);
        await viewHandler.ChangeView(ViewHandler.MANAGEPOST);
    }
}