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

    public CommentsController(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    [HttpPost]
    public async Task<IResult> CreateComment([FromBody] CreateCommentDto request)
    {
        try
        {
            Comment comment = new(request.CommentBody);
            Comment created = await commentRepository.AddCommentAsync(comment);
            return Results.Created($"/api/comments/{comment.Id}", created);
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
            Comment comment = new(request.CommentBody);
            comment.Id = request.Id;
            await commentRepository.UpdateCommentAsync(comment);

            return Results.Created($"/api/comments/{comment.Id}", comment);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}