using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;
    private ViewHandler viewHandler;

    public CreatePostView(IPostRepository postRepository, ViewHandler viewHandler)
    {
        this.postRepository = postRepository;
        this.viewHandler = viewHandler;
    }

    public class ExecuteCommand()
    {
         public CreatePostView createPostView { get; set; }
    }

    public void CreatePost()
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
        Post post = new Post(title, content, 2); //gotta figure out how to add the userID
        postRepository.AddPostAsync(post);
    }

}