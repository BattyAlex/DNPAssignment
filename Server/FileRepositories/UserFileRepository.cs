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
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        int maxId = users.Count > 0 ? users.Max(u => u.ID) : 1;
        user.ID = maxId + 1;
        users.Add(user);
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
        return user;
    }

    public async Task UpdateUserAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users =
            JsonSerializer.Deserialize<List<User>>(usersAsJson) ??
            new List<User>();


        User? existingUser = users.SingleOrDefault(u => u.ID == user.ID);

        if (existingUser is null)
        {
            throw new InvalidOperationException(
                $"User with ID '{user.ID}' not found");
        }


        users.Remove(existingUser);
        users.Add(user);


        string updatedUsersAsJson = JsonSerializer.Serialize(users,
            new JsonSerializerOptions { WriteIndented = true });

        await File.WriteAllTextAsync(filePath, updatedUsersAsJson);
    }


    public async Task DeleteUserAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        User? userToDelete = users.FirstOrDefault(u => u.ID == id);
        if (userToDelete != null)
        {
            users.Remove(userToDelete);

            usersAsJson = JsonSerializer.Serialize(users);
            await File.WriteAllTextAsync(filePath, usersAsJson);
        }
    }

    public async Task<User> GetSingleUserAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;


        User? user = users.FirstOrDefault(u => u.ID == id);
        if (user != null)
        {
            return user;
        }

        return null;
    }

    public IQueryable<User> GetManyUsersAsync()
    {
        string usersAsJson = File.ReadAllTextAsync(filePath).Result;
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;


        return users.AsQueryable();
    }
}