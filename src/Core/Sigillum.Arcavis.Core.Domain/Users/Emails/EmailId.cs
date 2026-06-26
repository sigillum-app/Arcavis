using Sigillum.Arcavis.Core.Domain.Common;

namespace Sigillum.Arcavis.Core.Domain.Users.Emails;

public record EmailId(Guid Value) : TypeId(Value);
