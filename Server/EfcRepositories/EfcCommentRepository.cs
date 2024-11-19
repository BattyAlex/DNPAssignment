using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcCommentRepository : ICommentRepository
{
    private readonly AppContext ctx;

    public EfcCommentRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Comment> AddCommentAsync(Comment comment)
    {
        EntityEntry<Comment> entityEntry = await ctx.Comments.AddAsync(comment);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateCommentAsync(Comment comment)
    {
        if (!(await ctx.Comments.AnyAsync(c => c.Id == comment.Id)))
        {
            throw new NotFoundException(
                $"Comment with ID {comment.Id} not found");
        }

        ctx.Comments.Update(comment);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteCommentAsync(int Id)
    {
        Comment? existing =
            await ctx.Comments.SingleOrDefaultAsync(c => c.Id == Id);
        if (existing == null)
        {
            throw new NotFoundException($"Comment with ID {Id} not found");
        }

        ctx.Comments.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<Comment> GetSingleCommentAsync(int Id)
    {
        Comment? comment =
            await ctx.Comments.SingleOrDefaultAsync(c => c.Id == Id);

        if (comment == null)
        {
            throw new NotFoundException($"Comment with ID {Id} not found");
        }

        return comment;
    }

    public IQueryable<Comment> GetAll()
    {
        return ctx.Comments.AsQueryable();
    }
}