using System.Text.Json;
using Entities;
using RepositoryContracts;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace FileRepositories;


public class CommentFileRepository: ICommentRepository
{
    private readonly string filePath = "comments.json";

    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    
    public async Task<Comment> AddCommentAsync(Comment comment)
    {
        List<Comment> comments = await LoadComments();
        int maxId = comments.Count > 0 ? comments.Max(c => c.Id) : 1;
        comment.Id = maxId + 1;
        comments.Add(comment);
        SaveComments(comments);
        return comment;
    }
    public async Task UpdateCommentAsync(Comment comment)
    {
        var comments = await LoadComments();
        var commentToUpdate = comments.FirstOrDefault(c => c.Id == c.Id);
        if (commentToUpdate is null)
        {
            throw new InvalidOperationException($"Post {comment.Id} does not exist");
        }
        comments.Remove(commentToUpdate);
        comments.Add(comment);
        SaveComments(comments);
    }

    public async Task DeleteCommentAsync(int id)
    {
        var comments = await LoadComments();
        Comment? commentToDelete = comments.FirstOrDefault(c => c.Id == id);
        if (commentToDelete is null)
        {
            comments.Remove(commentToDelete);
            SaveComments(comments);
        }
    }

    public async Task<Comment> GetSingleCommentAsync(int id)
    {
        var comments = await LoadComments();
        var commentToGet = comments.FirstOrDefault(c => c.Id == id);

        if (commentToGet == null)
        {
            throw new InvalidOperationException($"Comment {id} does not exist.");
        }

        return commentToGet;
    }

    private async Task<List<Comment>> LoadComments()
    {
        try
        {
            string commentsAsJson = await File.ReadAllTextAsync(filePath);
            var comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson) ?? new List<Comment>();
            return comments;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error loading comments from file.", ex);
        }
    }


    public IQueryable<Comment> GetAll()
    {
        var comments = LoadComments().GetAwaiter().GetResult();
        return comments.AsQueryable();
    }

    private async void SaveComments(List<Comment> comments)
    {
        string commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }

}