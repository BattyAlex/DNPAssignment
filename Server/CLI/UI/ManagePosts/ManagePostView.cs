namespace CLI.UI.ManagePosts;

public class ManagePostView
{
    private ViewHandler viewHandler;

    public ManagePostView(ViewHandler viewHandler)
    {
        this.viewHandler = viewHandler;
    }

    public void Start()
    {
        Console.WriteLine("Welcome to Reddat! What would you like to do?");
        Console.WriteLine("[1 - Add a new post]\n[2 - View existing posts]");
        string? input = Console.ReadLine();
        int choice = 0;
        while (choice == 0)
        {
            try
            {
                choice = Convert.ToInt32(input);
            }
            catch (Exception e)
            {
                Console.WriteLine("[1 - Add a new post]\n[2 - View existing posts]");
                input = Console.ReadLine();
            }
        }
        if (choice == 1)
        {
            Console.WriteLine("You chose to add a new post");
            viewHandler.ChangeView(ViewHandler.CREATEPOST);
        }
        else if (choice == 2)
        {
        Console.WriteLine("You chose to view existing posts");
        viewHandler.ChangeView(ViewHandler.LISTPOSTS);
        }
        else
        {
            Console.WriteLine("Goodbye.");
        }
    }
    
}