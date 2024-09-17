using System.Formats.Tar;
using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private List<User> users{get;set;}

    public UserInMemoryRepository()
    {
        User alexa = new User("Alexa", "alexaHasSh1nyHair");
        User salomeea = new User("Salomeea", "salomeea1sVeryPretty");
        User sophie = new User("Sophie", "sophi3l00ksGoodINBlu");
        User alex = new User("Alex", "alexLooksn1ceWIthR4inb0wHair");
        User kristy = new User("Kristy", "kristyMakesawesomeKittyn05ie5AndIsveryPrettyAndwILLHAVEtowriteAlotOThing5MUHAHA");
        users = new List<User>();
        AddUserAsync(alexa);
        AddUserAsync(salomeea);
        AddUserAsync(sophie);
        AddUserAsync(alex);
        AddUserAsync(kristy);
    }

    public Task<User> AddUserAsync(User user)
    {
        user.Id = users.Any()
            ? users.Max(x => x.Id) + 1
            : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateUserAsync(User user)
    {
        User? existingUser = users.SingleOrDefault(u => u.Id == user.Id);
        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{user.Id}' not found");
        }
        users.Remove(existingUser);
        users.Add(user);
        return Task.CompletedTask;
    }

    public Task DeleteUserAsync(int id)
    {
        User? userToRemove = users.SingleOrDefault(u => u.Id == id);
        if (userToRemove is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }
        users.Remove(userToRemove);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleUserAsync(int id)
    {
        User? userToGet = users.SingleOrDefault(u => u.Id == id);
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