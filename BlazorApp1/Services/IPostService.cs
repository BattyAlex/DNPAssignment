using DataTransferObjects;

namespace BlazorApp1.Services;

public interface IPostService
{
    public Task<CompletePostDTO> AddPostAsync(CreatePostDTO request);
    public Task UpdatePostAsync(int id, UpdatePostDTO request);
    public Task DeletePostAsync(int id);
    public Task<CompletePostDTO> GetPostAsync(int id);
    public Task<List<CompletePostDTO>> GetPostsAsync();
}