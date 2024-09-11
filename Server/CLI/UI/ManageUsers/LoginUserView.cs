using CLI.UI.ManagePosts;

namespace CLI.UI.ManageUsers;

public class LoginUserView
{
    private ViewHandler viewHandler;

    public LoginUserView(ViewHandler viewHandler)
    {
        this.viewHandler = viewHandler;
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
        
    }
}