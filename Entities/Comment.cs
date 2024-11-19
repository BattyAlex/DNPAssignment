namespace Entities;

public class Comment
{
    public int Id { get; set; }
    public string CommentBody { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }

    public Comment()
    {
    }

    public User User { get; set; }
    public Post Post { get; set; }

    public Comment(string commentBody)
    {
        this.CommentBody = commentBody;
    }

    public Comment(string body, int userId, int postId)
    {
        CommentBody = body;
        UserId = userId;
        PostId = postId;
    }
}