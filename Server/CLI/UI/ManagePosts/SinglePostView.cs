using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;
    private readonly IUserRepository userRepository;
    private ViewHandler viewHandler;

    public SinglePostView(IPostRepository postRepository, ICommentRepository commentRepository, IUserRepository userRepository, ViewHandler viewHandler )
    {
        this.postRepository = postRepository;
        this.viewHandler = viewHandler;
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
    }

    public async Task ShowPostById(int postId)
    {
        Post post = await postRepository.GetSinglePostAsync(postId);
        if (post != null)
        {
            Console.WriteLine($"Title: {post.Title}", post.Title);
            Console.WriteLine($"Content: {post.Content}", post.Content);
            Console.WriteLine("======== Comments ========");
            List<Comment> comments = commentRepository.GetAll().ToList();
            if (!comments.Any())
            {
                Console.WriteLine("There are currently no comments.");
            }
            else
            {
                foreach (Comment comment in comments)
                { 
                    if (comment.PostId == postId)
                    {
                        Console.WriteLine();
                        User commenter = await userRepository.GetSingleUserAsync(comment.UserId);
                        Console.WriteLine($"{commenter.Name} commented:\n{comment.CommentBody}");
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Post not found");
        }
    }
}