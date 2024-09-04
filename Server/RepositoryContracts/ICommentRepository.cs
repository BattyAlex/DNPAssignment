using Entities;

namespace RepositoryContracts;

public interface ICommentRepository
{
    Task<Comment> AddComment(Comment comment);
    Task UpdateCommentAsync(Comment comment);
    Task DeleteCommentAsync(int commentId);
    Task<Comment> GetSingleCommentAsync(string id);
    IQueryable<Comment> GetAll();
}