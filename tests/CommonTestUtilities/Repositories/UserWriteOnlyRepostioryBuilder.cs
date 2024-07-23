using CashFlow.Domain.Repositories.Users;
using Moq;

namespace CommonTestUtilities.Repositories;
public class UserWriteOnlyRepostioryBuilder
{
    public static IUserWriteOnlyRepostiory Build()
    {
        var mock = new Mock<IUserWriteOnlyRepostiory>();
        
        return mock.Object;
    }
}
