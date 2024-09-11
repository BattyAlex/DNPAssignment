using CLI.UI.ManagePosts;
using Entities;

namespace CLI.UI.ManageUsers;

public class LoginUserView
{
    private ViewHandler viewHandler;
    private UserLoggedIn userLoggedIn;
    public LoginUserView(ViewHandler viewHandler, UserLoggedIn userLoggedIn)
    {
        this.viewHandler = viewHandler;
        this.userLoggedIn = userLoggedIn;
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

        Console.WriteLine("Please enter Password");
        string? password = Console.ReadLine();
        while (password is null)
        {
            Console.WriteLine("Password is required");
            password = Console.ReadLine();
        }
        userLoggedIn.Login(new User(username, password));
        viewHandler.ChangeView(ViewHandler.MANAGEPOST);
    }
}