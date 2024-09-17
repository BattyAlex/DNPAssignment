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

    public async Task<List<Comment>> GetCommentsAsync(Comment comment)
    {
        string commentJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        comment.Id = maxId + 1;
        comments.Add(comment);
        commentJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentJson);
        return comments;
    }
}