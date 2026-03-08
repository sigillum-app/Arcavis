using Konscious.Security.Cryptography;
using Sigillum.Arcavis.Core.Application.Abstraction.Security.Hasher;
using System.Security.Cryptography;

namespace Sigillum.Arcavis.Infrastructure.Security.Hashing;

public sealed class Argon2Hasher : IArgon2Hasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;

    private const int Iterations = 3;
    private const int MemorySize = 32768;
    private const int Parallelism = 2;

    public string Hash(byte[] password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);

        var argon2 = new Argon2id(password)
        {
            Salt = salt,
            Iterations = Iterations,
            MemorySize = MemorySize,
            DegreeOfParallelism = Parallelism
        };

        var hash = argon2.GetBytes(HashSize);

        return $"$argon2id$v=19$m={MemorySize},t={Iterations},p={Parallelism}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
    }

    public bool Verify(byte[] password, string encodedHash)
    {
        var parts = encodedHash.Split('$', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 5)
            return false;

        var parameters = parts[2];
        var salt = Convert.FromBase64String(parts[3]);
        var hash = Convert.FromBase64String(parts[4]);

        var paramParts = parameters.Split(',');

        int memory = int.Parse(paramParts[0].Split('=')[1]);
        int iterations = int.Parse(paramParts[1].Split('=')[1]);
        int parallelism = int.Parse(paramParts[2].Split('=')[1]);

        var argon2 = new Argon2id(password)
        {
            Salt = salt,
            Iterations = iterations,
            MemorySize = memory,
            DegreeOfParallelism = parallelism
        };

        var computed = argon2.GetBytes(hash.Length);

        return CryptographicOperations.FixedTimeEquals(hash, computed);
    }
}