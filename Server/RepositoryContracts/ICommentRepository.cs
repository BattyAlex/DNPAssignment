﻿using Entities;

namespace RepositoryContracts;

public interface ICommentRepository
{
    Task<Comment> AddCommentAsync(Comment comment);
    Task UpdateCommentAsync(Comment comment);
    Task DeleteCommentAsync(int Id);
    Task<Comment> GetSingleCommentAsync(int Id);
    IQueryable<Comment> GetAll();
    List<Comment> GetAllComments();
}