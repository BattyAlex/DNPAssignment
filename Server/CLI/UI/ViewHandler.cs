using CLI.UI.ManageUsers;
using FileRepositories;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ViewHandler
{
    public static string LOGIN = "login";
    public static string MANAGEUSERS = "manageusers";
    public static string CREATEUSER = "createuser";
    public static string CREATEPOST = "createpost";
    public static string MANAGEPOST = "managepost";
    public static string LISTPOSTS = "listposts";
    
    private IUserRepository userRep;
    private ICommentRepository commentRep;
    private IPostRepository postRep;

    private LoginUserView loginUserView;
    private ManageUsersView manageUsersView;
    private CreateUserView createUserView;
    private CreatePostView createPostView;
    private ListPostView listPostView;
    private SinglePostView singlePostView;
    private ManagePostView managePostView;
    
    private UserLoggedIn userLoggedIn;
    public ViewHandler(IUserRepository userRep, ICommentRepository commentRep, IPostRepository postRep)
    {
        this.userRep = userRep;
        this.commentRep = commentRep;
        this.postRep = postRep;
        userLoggedIn = new UserLoggedIn();

        loginUserView = new LoginUserView(this, userLoggedIn, userRep);
        manageUsersView = new ManageUsersView(this);
        createUserView = new CreateUserView(this.userRep, this, userLoggedIn);
        createPostView = new CreatePostView(this.postRep, this, userLoggedIn);
        listPostView = new ListPostView(this.postRep,this);
        managePostView = new ManagePostView(this);
        singlePostView = new SinglePostView(this.postRep,this.commentRep,this.userRep,this.userLoggedIn, this);
    }

    public async Task StartAsync()
    {
        await ChangeView(MANAGEUSERS);
    }

    public async Task ChangeView(string id)
    {
        switch (id)
        {
            case "login":
                await loginUserView.Start();
                break;
            case "manageusers":
                await manageUsersView.Start();
                break;
            case "createuser":
                await createUserView.Start();
                break;
            case "createpost":
                await createPostView.Start();
                break;
            case "managepost":
                await managePostView.Start();
                break;
            case "listposts":
                await listPostView.Start();
                break;

        }
    }
    public async Task ShowPost(int postId)
    {
       await singlePostView.ShowPostById(postId);
    }
}