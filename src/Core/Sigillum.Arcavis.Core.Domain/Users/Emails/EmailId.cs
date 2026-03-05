using Sigillum.Arcavis.Core.Domain.SeedWork;

namespace Sigillum.Arcavis.Core.Domain.Users.Emails;

public record EmailId(Guid Value) : TypeId(Value);
