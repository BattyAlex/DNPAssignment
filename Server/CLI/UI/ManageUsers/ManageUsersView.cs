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
        int? input = Convert.ToInt32(Console.ReadLine());
        if (input == null)
        {
            Console.WriteLine("Goodbye");
        }
        else if (input == 1)
        {
            Console.WriteLine("You chose to log in.");
            viewHandler.ChangeView(ViewHandler.LOGIN);
        }
    }
}