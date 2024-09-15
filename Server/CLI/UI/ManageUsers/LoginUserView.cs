using CLI.UI.ManagePosts;
using Entities;
using InMemoryRepositories;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class LoginUserView
{
    private ViewHandler viewHandler;
    private UserLoggedIn userLoggedIn;
    private IUserRepository userRepository;
    public LoginUserView(ViewHandler viewHandler, UserLoggedIn userLoggedIn, IUserRepository userRepository)
    {
        this.viewHandler = viewHandler;
        this.userLoggedIn = userLoggedIn;
        this.userRepository = userRepository;
    }
    public void Start()
    {
        Console.WriteLine("Please enter Username");
        string? username = Console.ReadLine();
        while (username is null)
        {
            Console.WriteLine("Username is required");
            username = Console.ReadLine();
        }
        while (IsUnique(username))
        {
            Console.WriteLine("This username doesn't exists. Try again.");
            username = Console.ReadLine();
        }
        Console.WriteLine("Please enter Password");
        string? password = Console.ReadLine();
        while (password is null)
        {
            Console.WriteLine("Password is required");
            password = Console.ReadLine();
        }

        while (!Verify(username, password))
        {
            Console.WriteLine("This password does not match the username. Try again.");
            password = Console.ReadLine();
        }
        User userLoggingIn = new User(username, password);
        userRepository.AddUserAsync(userLoggingIn);
        userLoggedIn.Login(userLoggingIn);
        viewHandler.ChangeView(ViewHandler.MANAGEPOST);
    }

    private bool IsUnique(string username)
    {
        List<User> users = userRepository.GetManyUsersAsync().ToList();
        foreach (User user in users)
        {
            if (user.Name == username)
            {
                return false;
            }
        }

        return true;
    }

    private bool Verify(string username, string password)
    {
        List<User> users = userRepository.GetManyUsersAsync().ToList();
        foreach (User user in users)
        {
            if (user.Name == username)
            {
                if(user.Password == password)
                    return true;
            }
        }

        return false;
    }
}