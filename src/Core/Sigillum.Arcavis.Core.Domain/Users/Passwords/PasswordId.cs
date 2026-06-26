using Sigillum.Arcavis.Core.Domain.Common;

namespace Sigillum.Arcavis.Core.Domain.Users.Passwords;

public record PasswordId(Guid Value) : TypeId(Value);
