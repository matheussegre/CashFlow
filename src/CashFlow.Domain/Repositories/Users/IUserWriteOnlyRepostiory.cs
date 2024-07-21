using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Users;
public interface IUserWriteOnlyRepostiory
{
    Task Add(User user);
}
