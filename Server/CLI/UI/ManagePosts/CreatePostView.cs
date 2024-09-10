using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public class ExecuteCommand()
    {
         public CreatePostView createPostView { get; set; }
    }

    public void CreatePost()
    {
        Console.WriteLine("Enter the title of the post:");
        string? title = Console.ReadLine();

        Console.WriteLine("Enter the content of the post:");
        string? content = Console.ReadLine();
    }

}