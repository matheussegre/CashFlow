using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;
internal class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepostiory
{
    private readonly CashFlowDbContext _dbcontext;

    public UserRepository(CashFlowDbContext dbContext) => _dbcontext = dbContext;

    public async Task Add(User user)
    {
        await _dbcontext.Users.AddAsync(user);
    }

    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _dbcontext.Users.AnyAsync(user => user.Email.Equals(email));
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _dbcontext.Users
                                .AsNoTracking()
                                .FirstOrDefaultAsync(user => user.Email.Equals(email));
    }
}
