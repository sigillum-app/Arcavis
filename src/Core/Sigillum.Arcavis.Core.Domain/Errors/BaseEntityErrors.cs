using Sigillum.Arcavis.Core.Domain.Entities.Base;
using Sigillum.Arcavis.Core.Domain.ExceptionHandling;

namespace Sigillum.Arcavis.Core.Domain.Errors;

public static class TenantEntityErrors
{
    #region 1000 - 1099 => Tenant Entity Errors
    public static readonly DomainError InvalidTenant =
        new(1001, "INVALID_TENANT_ID", nameof(TenantEntity));
    #endregion
}
