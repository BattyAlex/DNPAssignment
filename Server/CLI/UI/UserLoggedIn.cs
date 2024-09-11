using Entities;

namespace CLI.UI;

public class UserLoggedIn
{
    public User? User { get; set; }

    public void Login(User user)
    {
        User = user;
    }
    
}