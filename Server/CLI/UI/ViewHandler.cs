using CLI.UI.ManageUsers;
using InMemoryRepositories;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ViewHandler
{
    public static string LOGIN = "login";
    public static string MANAGEUSERS = "manageusers";
    public static string CREATEUSER = "createuser";
    
    private IUserRepository userRep;
    private ICommentRepository commentRep;
    private IPostRepository postRep;

    private LoginUserView loginUserView;
    private ManageUsersView manageUsersView;
    private CreateUserView createUserView;
    public ViewHandler(IUserRepository userRep, ICommentRepository commentRep, IPostRepository postRep)
    {
        this.userRep = userRep;
        this.commentRep = commentRep;
        this.postRep = postRep;

        loginUserView = new LoginUserView();
        manageUsersView = new ManageUsersView(this);
        createUserView = new CreateUserView(this.userRep);
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
            case "createuser":
                createUserView.Start();
                break;
        }
    }
}