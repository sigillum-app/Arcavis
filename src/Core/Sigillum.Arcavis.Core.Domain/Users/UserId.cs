using Sigillum.Arcavis.Core.Domain.Common;

namespace Sigillum.Arcavis.Core.Domain.Users;

public record UserId(Guid Value) : TypeId(Value);
