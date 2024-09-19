using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;
    private readonly IUserRepository userRepository;
    private readonly ViewHandler viewHandler;
    private readonly UserLoggedIn userLoggedIn;

    private int postNumber; 
    public SinglePostView(IPostRepository postRepository, ICommentRepository commentRepository, IUserRepository userRepository, UserLoggedIn userLoggedIn, ViewHandler viewHandler )
    {
        this.postRepository = postRepository;
        this.viewHandler = viewHandler;
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
        this.userLoggedIn = userLoggedIn;
    }

    public async Task ShowPostById(int postId)
    {
        postNumber = postId;
        Post post = await postRepository.GetSinglePostAsync(postId);
            Console.WriteLine($"Title: {post.Title} (Posted by: {userRepository.GetSingleUserAsync(post.UserID).Result.Name})");
            Console.WriteLine($"Content: {post.Content}");
            Console.WriteLine("======== Comments ========");
            var comments = commentRepository.GetAll().ToList();
            if (comments.Count == 0)
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

            Console.WriteLine("[1 - Add a comment]\n[2 - Back]");
            string? input = Console.ReadLine();
            int inp = 0;
            while (inp == 0)
            {
                try
                {
                    inp = int.Parse(input!);
                }
                catch (FormatException e)
                {
                    Console.WriteLine("[1 - Add a comment]\n[2 - Back]");
                    input = Console.ReadLine();
                }
            }
            if (inp == 1)
            {
               await Comment();
            }
            else if (inp == 2)
            {
                await viewHandler.ChangeView(ViewHandler.LISTPOSTS);
            }
            else
            {
                Console.WriteLine("Goodbye.");
            }
    }

    private async Task Comment()
    {
        Console.WriteLine("Please write the comment here or type EXIT to abandon comment:");
        string? commentBody = Console.ReadLine();
        while (commentBody is null || commentBody.Equals(""))
        {
            Console.WriteLine("Please write the comment here or type EXIT to abandon comment:");
            commentBody = Console.ReadLine();
        }
        if (commentBody.Equals("EXIT"))
        {
            await ShowPostById(postNumber);
        }
        else
        {
            Comment comment = new Comment(commentBody, userLoggedIn.User.Id, postNumber);
            await commentRepository.AddCommentAsync(comment);
            await ShowPostById(postNumber);
        }
    }
}