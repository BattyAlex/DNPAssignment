namespace CLI.UI.ManageUsers;

public class LoginUserView
{
    public void Start()
    {
        Console.WriteLine("1 - Create user \n2 - Login");
        int? input = Console.Read();
        if(input is null)
            Console.WriteLine("Bad");
        if (input == 1)
        {
              //switch view
        }
        if (input == 2)
        {
        }
    }
}