using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository: ICommentRepository
{
    public List<Comment> Comments { get; set; } = new List<Comment>();
    public Task<Comment> AddCommentAsync(Comment comment)
    {
        comment.Id = Comments.Any()
            ? Comments.Max(c => c.Id) + 1
            : 1;
        Comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateCommentAsync(Comment comment)
    {
        Comment? existingComment =
            Comments.SingleOrDefault(c => c.Id == comment.Id);
        if (existingComment is null)
        {
            throw new InvalidOperationException(
                $"Comment with Id {comment.Id} was not found");
        }

        Comments.Remove(existingComment);
        Comments.Add(comment);
        return Task.CompletedTask;
    }

    public Task DeleteCommentAsync(int Id)
    {
        Comment? commentToRemove = Comments.SingleOrDefault(c => c.Id == Id);
        if (commentToRemove is null)
        {
            throw new InvalidOperationException($"Comment with Id {Id} was not found"
                );
            
        }
        Comments.Remove(commentToRemove);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleCommentAsync(int Id)
    {
        Comment? comment = Comments.SingleOrDefault(c => c.Id == Id);
        if (comment is null)
        {
            throw new InvalidOperationException($"Comment with Id {Id} was not found");
        }
        return Task.FromResult(comment);
    }

    public IQueryable<Comment> GetAll()
    {
        return Comments.AsQueryable();
    }
}