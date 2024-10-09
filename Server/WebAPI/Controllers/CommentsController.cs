using Entities;
using RepositoryContracts;
using DataTransferObjects;
using Entities;
namespace WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class CommentsController:ControllerBase
{
    private readonly ICommentRepository commentRepository;
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;

    public CommentsController(ICommentRepository commentRepository, IUserRepository userRepository, IPostRepository postRepository)
    {
        this.commentRepository = commentRepository;
        this.userRepository = userRepository;
        this.postRepository = postRepository;
    }

    [HttpPost]
    public async Task<IResult> CreateComment([FromBody] CreateCommentDto request)
    {
        try
        {
            User commenter = GetUserByName(request.Commenter);
            try
            {
                Post post = postRepository.GetSinglePostAsync(request.PostId).Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Results.NotFound(e.Message);
            }
            
            if (commenter == null)
            {
                return Results.NotFound("User does not exist");
            }
            else
            {
                Comment comment = new()
                            {
                                CommentBody = request.CommentBody,
                                UserId = commenter.Id,
                                PostId = request.PostId
                            };
                            Comment created = await commentRepository.AddCommentAsync(comment);
                            return Results.Created($"/api/comments/{comment.Id}", created);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetSingles([FromRoute] int id)
    {
        try
        {
            Comment result = await commentRepository.GetSingleCommentAsync(id);
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