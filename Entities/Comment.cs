namespace Entities;

public class Comment
{
        public int Id {get; set;}
        public string CommentBody{get; set;} 
        public int UserId {get; set;}
        public int PostId {get; set;}

        public Comment()
        {
        }

        public Comment(string body, int userId, int postId)
        {
            CommentBody = body;
            UserId = userId;
            PostId = postId;
        }
        
}