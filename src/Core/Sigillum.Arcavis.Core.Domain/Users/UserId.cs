using Sigillum.Arcavis.Core.Domain.SeedWork;

namespace Sigillum.Arcavis.Core.Domain.Users;

public record UserId(Guid Value) : TypeId(Value);
