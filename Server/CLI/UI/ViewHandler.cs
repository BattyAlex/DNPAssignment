using CLI.UI.ManageUsers;
using InMemoryRepositories;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ViewHandler
{
    public static string LOGIN = "login";
    public static string MANAGEUSERS = "manageusers";
    
    private IUserRepository userRep;
    private ICommentRepository commentRep;
    private IPostRepository postRep;

    private LoginUserView loginUserView;
    private ManageUsersView manageUsersView;
    public ViewHandler(IUserRepository userRep, ICommentRepository commentRep, IPostRepository postRep)
    {
        this.userRep = userRep;
        this.commentRep = commentRep;
        this.postRep = postRep;

        loginUserView = new LoginUserView();
        manageUsersView = new ManageUsersView(this);
    }

    public async Task StartAsync()
    {
        ChangeView(MANAGEUSERS);
    }

    public void ChangeView(string id)
    {
        switch (id)
        {
            case "login":
               loginUserView.Start(); 
                break;
            case "manageusers":
                manageUsersView.Start();
                break;
        }
    }
}