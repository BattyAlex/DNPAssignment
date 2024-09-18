using System.Text.Json;
using Entities;
using RepositoryContracts;
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
        var comments = await LoadComments();
        var maxId = comments.Count > 0 ? comments.Max(c => c.Id) : 1;
        comment.Id = maxId + 1;
        comments.Add(comment);
        await SaveComments(comments);
        return comment;
    }
    public async Task UpdateCommentAsync(Comment comment)
    {
        var comments = await LoadComments();
        var commentToUpdate = comments.FirstOrDefault(c => c.Id == comment.Id);
        if (commentToUpdate is null)
        {
            throw new InvalidOperationException($"Post {comment.Id} does not exist");
        }
        comments.Remove(commentToUpdate);
        comments.Add(comment);
        await SaveComments(comments);
    }

    public async Task DeleteCommentAsync(int id)
    {
        var comments = await LoadComments();
        Comment? commentToDelete = comments.FirstOrDefault(c => c.Id == id);
        if (commentToDelete != null)
        {
            comments.Remove(commentToDelete);
            await SaveComments(comments);
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
    public IQueryable<Comment> GetAll()
    {
        var comments = LoadComments().Result;
        return comments.AsQueryable();
    }
    private async Task<List<Comment>> LoadComments()
    {
        try
        {
            var commentsAsJson = await File.ReadAllTextAsync(filePath);
            List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
            return comments;
        }
        catch (AggregateException ex)
        {
            throw new InvalidOperationException("Error loading comments from file.", ex);
        }
    }
    private async Task SaveComments(List<Comment> comments)
    {
        var commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }
}