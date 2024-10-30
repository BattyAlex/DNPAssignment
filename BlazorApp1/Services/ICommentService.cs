using DataTransferObjects;

namespace BlazorApp1.Services;

public interface ICommentService
{
    public Task<CommentDTO> AddCommentAsync(CreateCommentDto request);

    public Task UpdateCommentAsync(int id,
        ReplaceCommentDTO request);

    public Task DeleteCommentAsync(int id);
    public Task<CommentDTO> GetCommentByIdAsync(int id);
    public Task<List<CommentDTO>> GetAllCommentsAsync();
}