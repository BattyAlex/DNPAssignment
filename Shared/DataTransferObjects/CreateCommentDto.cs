namespace DataTransferObjects;

public class CreateCommentDto
{
    public required string CommentBody{ get; set; }
    public required string Commenter{ get; set; }
    public required int PostId{ get; set; }
}