using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;
internal class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepostiory, IUserUpdateOnlyRepository
{
    private readonly CashFlowDbContext _dbcontext;

    public UserRepository(CashFlowDbContext dbContext) => _dbcontext = dbContext;

    public async Task Add(User user)
    {
        await _dbcontext.Users.AddAsync(user);
    }

    public async Task Delete(User user)
    {
        var userToRemove = await _dbcontext.Users.FindAsync(user.Id);
        _dbcontext.Users.Remove(userToRemove!);
    }

    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _dbcontext.Users.AnyAsync(user => user.Email.Equals(email));
    }

    public async Task<User> GetById(long id)
    {
        return await _dbcontext.Users.FirstAsync(user => user.Id.Equals(id));
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _dbcontext.Users
                                .AsNoTracking()
                                .FirstOrDefaultAsync(user => user.Email.Equals(email));
    }

    public void Update(User user)
    {
        _dbcontext.Users.Update(user);
    }
}
