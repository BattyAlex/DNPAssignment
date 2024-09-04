namespace Entities;

public class Post
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string UserID { get; set; }
    public Post(int id, string title, string content, string userId)
    {
        ID = id;
        Title = title;
        Content = content;
        UserID = userId;
    }
}