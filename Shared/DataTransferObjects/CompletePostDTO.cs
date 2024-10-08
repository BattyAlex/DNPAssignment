namespace DataTransferObjects;

public class CompletePostDTO
{
    public string Title { get; set; }
    public string Content { get; set; }
    public UsernameDTO? Author { get; set; }
    public List<CommentDTO>? Comments { get; set; }
    
}