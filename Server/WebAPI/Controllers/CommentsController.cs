using Entities;
using RepositoryContracts;
using DataTransferObjects;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository commentRepository;
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;

    public CommentsController(ICommentRepository commentRepository, IUserRepository userRepository,
        IPostRepository postRepository)
    {
        this.commentRepository = commentRepository;
        this.userRepository = userRepository;
        this.postRepository = postRepository;
    }

    [HttpPost]
    public async Task<IResult> CreateComment([FromBody] CreateCommentDto request)
    {
        User? commenter = null;
        Post? post = null;
        try
        {
            try
            {
                commenter = GetUserByName(request.Commenter);
                post = postRepository.GetSinglePostAsync(request.PostId).Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Results.NotFound(e.Message);
            }

            Comment comment = new()
            {
                CommentBody = request.CommentBody,
                UserId = commenter.Id,
                PostId = post.ID
            };
            Comment newComment = await commentRepository.AddCommentAsync(new Comment()
            {
                CommentBody = request.CommentBody,
                UserId = commenter.Id,
                PostId = request.PostId
            });
            CommentDTO created = new CommentDTO()
            {
                CommentId = newComment.Id,
                CommentBody = newComment.CommentBody,
                Commenter = userRepository.GetSingleUserAsync(newComment.UserId).Result.Name
            };
            return Results.Created($"/api/comments/{comment.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("{commentId}")]
    public async Task<IResult> GetSingles([FromRoute] int commentId)
    {
        try
        {
            Comment result = await commentRepository.GetSingleCommentAsync(commentId);
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Results.NotFound(e.Message);
        }
    }

    [HttpGet]
    public IResult GetMany([FromQuery] string? body, [FromQuery] int? userId, [FromQuery] int? postId)
    {
        try
        {
            List<Comment> comments = commentRepository.GetAll().ToList();

            if (!string.IsNullOrWhiteSpace(body))
            {
                comments = comments.Where(c => c.CommentBody.ToLower().Contains(body.ToLower())).ToList();
            }

            if (userId.HasValue)
            {
                comments = comments.Where(c => c.UserId == userId).ToList();
            }

            if (postId.HasValue)
            {
                comments = comments.Where(c => c.PostId == postId).ToList();
            }

            return Results.Ok(comments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteComment([FromRoute] int id)
    {
        try
        {
            await commentRepository.DeleteCommentAsync(id);
            return Results.NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPut]
    public async Task<IResult> UpdateComment([FromBody] ReplaceCommentDTO request)
    {
        try
        {
            Comment comment = commentRepository.GetSingleCommentAsync(request.Id).Result;
            comment.CommentBody = request.CommentBody;
            await commentRepository.UpdateCommentAsync(comment);

            return Results.Created($"/api/comments/{comment.Id}", comment);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private User GetUserByName(string userName)
    {
        List<User> users = userRepository.GetManyUsersAsync().ToList();
        foreach (User user in users)
        {
            if (user.Name == userName)
            {
                return user;
            }
        }

        return null;
    }
}