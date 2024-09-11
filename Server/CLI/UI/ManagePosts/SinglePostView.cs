using Entities;
using InMemoryRepositories;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository = new CommentInMemoryRepository();
    private ViewHandler viewHandler;

    public SinglePostView(IPostRepository postRepository, ViewHandler viewHandler )
    {
        
        this.postRepository = postRepository;
        this.viewHandler = viewHandler;
    }

    public async Task ShowPostById(int postId)
    {
        Post post = await postRepository.GetSinglePostAsync(postId);
        if (post != null)
        {
            Console.WriteLine("Title: {post.Title}", post.Title);
            Console.WriteLine("Content: {post.Content}", post.Content);
            commentRepository.GetAll().ToList().ForEach(comment => Console.WriteLine("Comment: {comment}", comment));
        }
        else
        {
            Console.WriteLine("Post not found");
        }
    }
}