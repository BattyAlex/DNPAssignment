using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcUserRepository : IUserRepository
{
    private readonly AppContext ctx;

    public EfcUserRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<User> AddUserAsync(User user)
    {
        EntityEntry<User> entityEntry = await ctx.Users.AddAsync(user);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateUserAsync(User user)
    {
        if (!(await ctx.Users.AnyAsync(u => u.Id == user.Id)))
        {
            throw new NotFoundException($"User with ID {user.Id} not found");
        }

        ctx.Users.Update(user);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        User? existing = await ctx.Users.SingleOrDefaultAsync(u => u.Id == id);
        if (existing == null)
        {
            throw new NotFoundException($"User with ID {id} not found");
        }

        ctx.Users.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<User> GetSingleUserAsync(int id)
    {
        User? user = await ctx.Users.SingleOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            throw new NotFoundException($"User with ID {id} not found");
        }

        return user;
    }

    public IQueryable<User> GetManyUsersAsync()
    {
        return ctx.Users.AsQueryable();
    }
}