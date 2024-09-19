namespace Entities;

public class Post
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int UserID { get; set; }
    public Post(string title, string content, int userId)
    {
        Title = title;
        Content = content;
        UserID = userId;
    }

    private Post()
    {
        
    }
}