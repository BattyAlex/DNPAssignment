using Entities;

namespace CLI.UI;

public class UserLoggedIn
{
    private User? userLoggedIn;

    public void Login(User user)
    {
        userLoggedIn = user;
    }
}