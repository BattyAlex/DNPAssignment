using DataTransferObjects;
using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository: ICommentRepository
{
    private List<Comment> comments { get; set; } = new List<Comment>();

    public CommentInMemoryRepository()
    {
        Comment commentOnDogs1 = new Comment("Good to know!", 1, 1);
        Comment commentOnDogs2 = new Comment("Oh no! Having no insulation must be woofful!", 4, 1);
        Comment commentOnPuns = new Comment("Okay, that was a good one.", 3, 2);
        AddCommentAsync(commentOnDogs1);
        AddCommentAsync(commentOnDogs2);
        AddCommentAsync(commentOnPuns);
    }
    public Task<Comment> AddCommentAsync(Comment comment)
    {
        comment.Id = comments.Any()
            ? comments.Max(c => c.Id) + 1
            : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateCommentAsync(Comment comment, ReplaceCommentDTO replaceCommentDTO)
    {
        Comment? existingComment =
            comments.SingleOrDefault(c => c.Id == comment.Id);
        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Comment with Id {comment.Id} was not found");
        }

        comments.Remove(existingComment);
        comments.Add(comment);
        return Task.CompletedTask;
    }

    public Task DeleteCommentAsync(int Id)
    {
        Comment? commentToRemove = comments.SingleOrDefault(c => c.Id == Id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException($"Comment with Id {Id} was not found"
                );
            
        }
        comments.Remove(commentToRemove);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleCommentAsync(int Id)
    {
        Comment? comment = comments.SingleOrDefault(c => c.Id == Id);
        if (comment is null)
        {
            throw new InvalidOperationException($"Comment with Id {Id} was not found");
        }
        return Task.FromResult(comment);
    }

    public IQueryable<Comment> GetAll()
    {
        return comments.AsQueryable();
    }
}