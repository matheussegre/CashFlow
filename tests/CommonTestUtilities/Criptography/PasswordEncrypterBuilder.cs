using CashFlow.Domain.Security.Criptography;
using Moq;

namespace CommonTestUtilities.Criptography;
public class PasswordEncrypterBuilder
{
    private readonly Mock<IPasswordEncripter> _mock;
    public PasswordEncrypterBuilder()
    {
        _mock = new Mock<IPasswordEncripter>();

        _mock.Setup(passwordEncrypter => passwordEncrypter
                                .ExecuteCriptography(It.IsAny<string>())).Returns("!#daishdipo934g");
    }

    public PasswordEncrypterBuilder Verify(string? password)
    {
        if(string.IsNullOrWhiteSpace(password) is false)
        {
            _mock.Setup(passwordEncrypter => passwordEncrypter
                                    .Verify(password, It.IsAny<string>())).Returns(true);
        }

        return this;
    }

    public IPasswordEncripter Build() => _mock.Object;
}
