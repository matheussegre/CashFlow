namespace CashFlow.Domain.Security.Criptography;
public interface IPasswordEncripter
{
    string ExecuteCriptography(string password);
    bool Verify(string password, string passwordHash);
}
