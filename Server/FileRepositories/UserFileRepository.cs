using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "users.json";

    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<User> AddUserAsync(User user)
    {
        List<User> users = await LoadUsers();
        int maxId = users.Count > 0 ? users.Max(u => u.Id) : 0;
        user.Id = maxId + 1;
        users.Add(user);
        await SaveUsers(users);
        return user;
    }

    public async Task UpdateUserAsync(User user)
    {
        List<User> users = await LoadUsers();
        User? existingUser = users.SingleOrDefault(u => u.Id == user.Id);
        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{user.Id}' not found");
        }
        users.Remove(existingUser);
        users.Add(user);
        await SaveUsers(users);
    }


    public async Task DeleteUserAsync(int id)
    {
        List<User> users = await LoadUsers();
        User? userToDelete = users.FirstOrDefault(u => u.Id == id);
        if (userToDelete != null)
        {
            users.Remove(userToDelete);

            await SaveUsers(users);
        }
    }

    public async Task<User> GetSingleUserAsync(int id)
    {
        List<User> users = await LoadUsers();
        User? userToGet = users.SingleOrDefault(u => u.Id == id);
        if (userToGet is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }
        return userToGet;
    }

    public IQueryable<User> GetManyUsersAsync()
    {
        List<User> users = LoadUsers().Result;
        return users.AsQueryable();
    }

    private async Task<List<User>> LoadUsers()
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        return users;
    }

    private async Task SaveUsers(List<User> users)
    {
        string usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }
}