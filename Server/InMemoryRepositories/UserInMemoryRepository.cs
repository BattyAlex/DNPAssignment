using System.Formats.Tar;
using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private List<User> users{get;set;}

    public Task<User> AddUserAsync(User user)
    {
        user.ID = users.Any()
            ? users.Max(x => x.ID) + 1
            : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateUserAsync(User user)
    {
        User? existingUser = users.SingleOrDefault(u => u.ID == user.ID);
        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{user.ID}' not found");
        }
        users.Remove(existingUser);
        users.Add(user);
        return Task.CompletedTask;
    }

    public Task DeleteUserAsync(int id)
    {
        User? userToRemove = users.SingleOrDefault(u => u.ID == id);
        if (userToRemove is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }
        users.Remove(userToRemove);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleUserAsync(int id)
    {
        User? userToGet = users.SingleOrDefault(u => u.ID == id);
        if (userToGet is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }
        return Task.FromResult(userToGet);
    }

    public IQueryable<User> GetManyUsersAsync()
    {
        
        return users.AsQueryable();
    }
    
}