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
        Comment comment = new(request.CommentBody);
        Comment created = await commentRepository.AddCommentAsync(comment);
        return Results.Created($"/api/comments/{comment.Id}", created);
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
    public async Task<IResult> GetMany([FromQuery] string? body,
        [FromQuery] int? userId, [FromQuery] int? postId)
    {
        List<Comment> comments = commentRepository.GetAll().ToList();

        if (!string.IsNullOrWhiteSpace(body))
        {
            comments = comments
                .Where(c => c.CommentBody.ToLower().Contains(body.ToLower())).ToList();
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

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteComment([FromRoute] int id)
    {
        await commentRepository.DeleteCommentAsync(id);
        return Results.NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IResult> UpdateComment([FromRoute] int id, [FromBody] ReplaceCommentDTO request)
    {
        UpdateCommentDTO comment = new()
        {
            id = id
        };
        Comment updated = new()
        {
            Id = comment.id
        };
        await commentRepository.UpdateCommentAsync(updated, request);
        return Results.NoContent();
    }
}