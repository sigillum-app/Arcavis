namespace Sigillum.Arcavis.Core.Application.Contracts.Security.Hasher;

public interface IPasswordHasher
{
    string HashPassword(string password);

    bool VerifyPassword(string password, string hash);
}
