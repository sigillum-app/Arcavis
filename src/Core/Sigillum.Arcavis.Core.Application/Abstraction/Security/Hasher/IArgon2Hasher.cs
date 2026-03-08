namespace Sigillum.Arcavis.Core.Application.Abstraction.Security.Hasher;

public interface IArgon2Hasher
{
    string Hash(byte[] password);

    bool Verify(byte[] password, string encodedHash);
}