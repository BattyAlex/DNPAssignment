namespace Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }

    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public User(string name, string password)
    {
        Password = password;
        Name = name;
    }

    public User()
    {
    }
}