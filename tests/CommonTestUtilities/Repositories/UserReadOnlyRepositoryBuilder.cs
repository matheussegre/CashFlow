using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Users;
using Moq;

namespace CommonTestUtilities.Repositories;
public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _repostiory;
    public UserReadOnlyRepositoryBuilder()
    {
        _repostiory = new Mock<IUserReadOnlyRepository>();
    }

    public void ExistActiveUsersWithEmail(string email)
    {
        _repostiory.Setup(userReadRepository => userReadRepository
                            .ExistActiveUserWithEmail(email)).ReturnsAsync(true);
    }

    public UserReadOnlyRepositoryBuilder GetUserByEmail(User user)
    {
        _repostiory.Setup(userReadRepository => userReadRepository
                                .GetUserByEmail(user.Email)).ReturnsAsync(user);

        return this;
    }

    public IUserReadOnlyRepository Build() => _repostiory.Object;
}
