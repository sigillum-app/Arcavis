using Sigillum.Arcavis.Core.Application.Abstraction.Security.Hasher;
using System.Text;

namespace Sigillum.Arcavis.Infrastructure.Security.Hashing;

public sealed class PasswordHasher : IPasswordHasher
{
    private readonly IArgon2Hasher _argon2Hasher;

    public PasswordHasher(IArgon2Hasher argon2Hasher)
    {
        _argon2Hasher = argon2Hasher;
    }

    public string HashPassword(string password)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        return _argon2Hasher.Hash(passwordBytes);
    }

    public bool VerifyPassword(string password, string hash)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        return _argon2Hasher.Verify(passwordBytes, hash);
    }
}