using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;
    private ViewHandler viewHandler;

    public SinglePostView(IPostRepository postRepository, ICommentRepository commentRepository, ViewHandler viewHandler )
    {
        this.postRepository = postRepository;
        this.viewHandler = viewHandler;
        this.commentRepository = commentRepository;
    }

    public async Task ShowPostById(int postId)
    {
        Post post = await postRepository.GetSinglePostAsync(postId);
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