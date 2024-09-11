using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository _postRepository;
    private ViewHandler viewHandler;

    public SinglePostView(IPostRepository postRepository, ViewHandler viewHandler )
    {
        _postRepository = postRepository;
        this.viewHandler = viewHandler;
    }

    public async Task ShowPostById(int postId)
    {
        Post post = await _postRepository.GetSinglePostAsync(postId);
        if (post != null)
        {
            Console.WriteLine("Title: {post.Title}", post.Title);
            Console.WriteLine("Content: {post.Content}", post.Content);
            
        }
        else
        {
            Console.WriteLine("Post not found");
        }
    }
}