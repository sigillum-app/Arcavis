using Microsoft.Extensions.DependencyInjection;
using Sigillum.Arcavis.Core.Application.Contracts.Security.Hasher;
using Sigillum.Arcavis.Infrastructure.Security.Hashing;

namespace Sigillum.Arcavis.Infrastructure.Security.Extensions;

public static class Argon2Registry
{
    public static IServiceCollection AddArgon2Registration(this IServiceCollection services)
    {
        services.AddScoped<IArgon2Hasher, Argon2Hasher>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
