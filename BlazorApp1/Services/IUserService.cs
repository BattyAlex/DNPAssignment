using BlazorApp1.Components.Pages;
using DataTransferObjects;

namespace BlazorApp1.Services;

public interface IUserService
{
    public Task<UserDTO> AddUserAsync(CreateUserDTO request);
    public Task UpdateUserAsync(int id, UpdateUserDTO request);
    public Task DeleteUserAsync(int id);
    public Task<UserDTO> GetUserAsync(int id);
    public Task<List<UserDTO>> GetUsersAsync();
    
}