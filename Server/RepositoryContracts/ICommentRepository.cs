using Entities;

namespace RepositoryContracts;

public interface ICommentRepository
{
    Task<Comment> AddComment(Comment comment);
    Task UpdateCommentAsync(Comment comment);
    Task DeleteCommentAsync(Comment comment);
    Task<Comment> GetSingleCommentAsync(string id);
    IQueryable<Comment> GetAll();
}