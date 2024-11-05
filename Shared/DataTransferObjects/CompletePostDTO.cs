namespace DataTransferObjects;

public class CompletePostDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string? Author { get; set; }
    public List<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
    
}