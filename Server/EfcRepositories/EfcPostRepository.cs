using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcPostRepository : IPostRepository
{
    private readonly AppContext ctx;

    public EfcPostRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Post> AddPostAsync(Post post)
    {
        EntityEntry<Post> entityEntry = await ctx.Posts.AddAsync(post);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdatePostAsync(Post post)
    {
        if (!(await ctx.Posts.AnyAsync(p => p.ID == post.ID)))
        {
            throw new NotFoundException("Post with id {post.Id} not found");
        }

        ctx.Posts.Update(post);
        await ctx.SaveChangesAsync();
    }

    public async Task DeletePostAsync(int id)
    {
        Post? existing = await ctx.Posts.SingleOrDefaultAsync(p => p.ID == id);
        if (existing == null)
        {
            throw new NotFoundException($"Post with id {id} not found");
        }

        ctx.Posts.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<Post> GetSinglePostAsync(int id)
    {
        Post? post = await ctx.Posts.SingleOrDefaultAsync(p => p.ID == id);

        if (post == null)
        {
            throw new NotFoundException($"Post with ID {id} not found");
        }

        return post;
    }

    public IQueryable<Post> GetMultiplePosts()
    {
        return ctx.Posts.AsQueryable();
    }
}