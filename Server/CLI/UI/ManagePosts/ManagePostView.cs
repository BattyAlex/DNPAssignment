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
        int? input = Convert.ToInt32(Console.ReadLine());
        if (input == null)
        {
            Console.WriteLine("Goodbye");
        }
        else if (input == 1)
        {
            
        }
        else if (input == 2)
        {
            
        }
        else
        {
            Console.WriteLine("Goodbye.");
        }
    }
    
}