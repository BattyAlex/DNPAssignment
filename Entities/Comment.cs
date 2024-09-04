﻿namespace Entities;

public class Comment
{
        public int Id {get; set;}
        public string CommentBody{get; set;} 
        public int UserId {get; set;}
        public int PostId {get; set;}

        public Comment(int id, string body, int userId, int postId)
        {
            Id = id;
            CommentBody = body;
            UserId = userId;
            PostId = postId;
        }
}