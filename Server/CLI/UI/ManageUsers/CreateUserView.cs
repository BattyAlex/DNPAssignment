using CLI.UI.ManagePosts;
using Entities;
using InMemoryRepositories;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository userRepository;
    private ViewHandler viewHandler;
    private UserLoggedIn userLoggedIn;
    
    public CreateUserView(IUserRepository userRepository, ViewHandler viewHandler, UserLoggedIn userLoggedIn)
    {
        this.userRepository = userRepository;
        this.viewHandler = viewHandler;
        this.userLoggedIn = userLoggedIn;
    }

    public void Start()
    {
        Console.WriteLine("Please choose a Username:");
        string? username = Console.ReadLine();
        while (username is null)
        {
            Console.WriteLine("Username is required");
            username = Console.ReadLine();
        }
        while (exists(username))
        {
            Console.WriteLine("This username already exists. Try again.");
            username = Console.ReadLine();
        }
        Console.WriteLine("Please choose a Password:");
        string? password = Console.ReadLine();
        while (password is null)
        {
            Console.WriteLine("Password is required");
            password = Console.ReadLine();
        }
        User newUser = new User(password, username);

        userRepository.AddUserAsync(newUser);
        userLoggedIn.Login(newUser);
        viewHandler.ChangeView(ViewHandler.MANAGEPOST);
    }

    private bool exists(string username)
    {
        List<User> users = userRepository.GetManyUsersAsync().ToList();
        foreach (User user in users)
        {
            if (user.Name == username)
            {
                return true;
            }
        }

        return false;
    }

}