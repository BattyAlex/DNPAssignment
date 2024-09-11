using CLI.UI.ManagePosts;
using Entities;
using InMemoryRepositories;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository _userRepository;
    private ViewHandler viewHandler;
    private UserLoggedIn userLoggedIn;

    public CreateUserView(IUserRepository userRepository, ViewHandler viewHandler)
    public CreateUserView(IUserRepository userRepository, ViewHandler viewHandler, UserLoggedIn userLoggedIn)
    {
        _userRepository = userRepository;
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
        Console.WriteLine("Please choose a Password:");
        string? password = Console.ReadLine();
        while (password is null)
        {
            Console.WriteLine("Password is required");
            password = Console.ReadLine();
        }
        User newUser = new User(password, username);
<<<<<<< Updated upstream
        _userRepository.AddUserAsync(newUser);
=======
        userRepository.AddUserAsync(newUser);
        userLoggedIn.Login(newUser);
>>>>>>> Stashed changes
        viewHandler.ChangeView(ViewHandler.MANAGEPOST);
    }

    
}