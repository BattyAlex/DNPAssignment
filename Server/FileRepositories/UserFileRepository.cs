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
        int maxId = users.Count > 0 ? users.Max(u => u.ID) : 1;
        user.ID = maxId + 1;
        users.Add(user);
        SaveUsers(users);
        return user;
    }

    public async Task UpdateUserAsync(User user)
    {
        List<User> users = await LoadUsers();
        User? existingUser = users.SingleOrDefault(u => u.ID == user.ID);
        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{user.ID}' not found");
        }
        users.Remove(existingUser);
        users.Add(user);
        SaveUsers(users);
    }


    public async Task DeleteUserAsync(int id)
    {
        List<User> users = await LoadUsers();
        User? userToDelete = users.FirstOrDefault(u => u.ID == id);
        if (userToDelete != null)
        {
            users.Remove(userToDelete);

            SaveUsers(users);
        }
    }

    public async Task<User> GetSingleUserAsync(int id)
    {
        List<User> users = await LoadUsers();
        User? user = users.FirstOrDefault(u => u.ID == id);
        if (user != null)
        {
            return user;
        }
        return null;
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

    private async void SaveUsers(List<User> users)
    {
        string usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }
}