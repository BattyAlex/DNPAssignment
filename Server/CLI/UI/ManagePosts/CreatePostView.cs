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

    public void CreatePost(string title, string content, int userId)
    {
        var post = new Post(title, content, userId);
        {
            post.Title = title;
            post.Content = content;
            post.UserID = userId;
        }
        
postRepository.AddPostAsync(post);  
    }
}