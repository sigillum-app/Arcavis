using Microsoft.Extensions.DependencyInjection;
using Sigillum.Arcavis.Core.Application.Contracts.Security.Hasher;
using Sigillum.Arcavis.Infrastructure.Security.Hashing;

namespace Sigillum.Arcavis.Infrastructure.Security;

public static class Argon2Registration
{
    public static IServiceCollection AddArgon2(this IServiceCollection services)
    {
        services.AddScoped<IArgon2Hasher, Argon2Hasher>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
