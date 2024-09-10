using Entities;
using InMemoryRepositories;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository _userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void Show()
    {
        Console.WriteLine("Create New User");
        Console.WriteLine("Enter Username:");
        string username = Console.ReadLine();
        Console.WriteLine("Enter Password:");
        string password = Console.ReadLine();
        
        User newUser = new User(password, username);
        _userRepository.AddUserAsync(newUser);
    }

    
}