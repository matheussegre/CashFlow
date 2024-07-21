using CashFlow.Domain.Security.Criptography;
using Encrypt = BCrypt.Net.BCrypt;


namespace CashFlow.Infrastructure.Security;
internal class Criptography: IPasswordEncripter
{
    public string ExecuteCriptography(string password)
    {
        string passwordHash = Encrypt.HashPassword(password);

        return passwordHash;
    }

    public bool Verify(string password, string passwordHash) => Encrypt.Verify(password, passwordHash);
}
