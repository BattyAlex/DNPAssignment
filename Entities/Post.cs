namespace Entities;

public class Post
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int UserID { get; set; }

    public User User { get; set; }

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public Post(string title, string content, int userId)
    {
        Title = title;
        Content = content;
        UserID = userId;
    }

    public Post(string title, string content)
    {
        Title = title;
        Content = content;
    }

    public Post()
    {
    }
}