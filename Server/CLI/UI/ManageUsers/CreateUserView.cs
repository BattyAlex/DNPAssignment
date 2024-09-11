using CLI.UI.ManagePosts;
using Entities;
using InMemoryRepositories;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository userRepository;
    private ViewHandler viewHandler;

    public CreateUserView(IUserRepository userRepository, ViewHandler viewHandler)
    {
        this.userRepository = userRepository;
        this.viewHandler = viewHandler;
    }

    public void Start()
    {
        Console.WriteLine("Please choose a Username:");
        string? username = Console.ReadLine();
        while (username is null || username.Equals(""))
        {
            Console.WriteLine("Username is required");
            username = Console.ReadLine();
        }
        Console.WriteLine("Please choose a Password:");
        string? password = Console.ReadLine();
        while (password is null || password.Equals(""))
        {
            Console.WriteLine("Password is required");
            password = Console.ReadLine();
        }
        User newUser = new User(password, username);
        userRepository.AddUserAsync(newUser);
        
        viewHandler.ChangeView(ViewHandler.MANAGEPOST);
    }

    
}