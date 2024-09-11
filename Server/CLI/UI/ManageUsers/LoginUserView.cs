using CLI.UI.ManagePosts;
using Entities;
using InMemoryRepositories;

namespace CLI.UI.ManageUsers;

public class LoginUserView
{
    private ViewHandler viewHandler;
    private UserLoggedIn userLoggedIn;
    private UserInMemoryRepository userInMemoryRepository;
    public LoginUserView(ViewHandler viewHandler, UserLoggedIn userLoggedIn, UserInMemoryRepository userInMemoryRepository)
    {
        this.viewHandler = viewHandler;
        this.userLoggedIn = userLoggedIn;
        this.userInMemoryRepository = userInMemoryRepository;
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
        while (isUnique(username))
        {
            Console.WriteLine("This username already exists. Try again.");
            username = Console.ReadLine();
        }
        Console.WriteLine("Please enter Password");
        string? password = Console.ReadLine();
        while (password is null)
        {
            Console.WriteLine("Password is required");
            password = Console.ReadLine();
        }
        User userLoggingIn = new User(username, password);
        userInMemoryRepository.AddUserAsync(userLoggingIn);
        userLoggedIn.Login(userLoggingIn);
        viewHandler.ChangeView(ViewHandler.MANAGEPOST);
    }

    private bool isUnique(string username)
    {
        List<User> users = userInMemoryRepository.GetManyUsersAsync().ToList();
        foreach (User user in users)
        {
            if (user.Name == username)
            {
                return false;
            }
        }

        return true;
    }
}