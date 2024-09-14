using CLI.UI.ManagePosts;
using Entities;

namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    private ViewHandler viewHandler;
    public ManageUsersView(ViewHandler viewHandler)
    {
        this.viewHandler = viewHandler;
    }
    public void Start()
    {
        Console.WriteLine("Hello and welcome to Reddat! What would you like to do?");
        Console.WriteLine("[1 - Login]\n[2 - Create new user]");
        string? input = Console.ReadLine();
        int inp = 0;
        while (inp == 0)
        {
            try
            {
                inp = int.Parse(input);
            }
            catch (FormatException e)
            {
                Console.WriteLine("[1 - Login]\n[2 - Create new user]");
                input = Console.ReadLine();
            }
        }
        if (inp == 1)
        {
            Console.WriteLine("You chose to log in.");
            viewHandler.ChangeView(ViewHandler.LOGIN);
        }
        else if (inp == 2)
        {
            Console.WriteLine("You chose to create new user.");
            viewHandler.ChangeView(ViewHandler.CREATEUSER);
        }
        else
        {
            Console.WriteLine("Goodbye");
        }
            
    }
}