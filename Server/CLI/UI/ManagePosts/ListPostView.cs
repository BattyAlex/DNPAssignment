using InMemoryRepositories;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostView
{
    private ViewHandler viewHandler;
    private readonly IPostRepository postRepository = new PostInMemoryRepository();

    public ListPostView(ViewHandler viewHandler)
    {
        this.viewHandler = viewHandler;
    }

    public void Show()
    {
        Console.WriteLine("Available Posts:");
    }
    
}