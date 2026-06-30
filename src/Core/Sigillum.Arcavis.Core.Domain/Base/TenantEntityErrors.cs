using Sigillum.Arcavis.Core.Domain.Common;

namespace Sigillum.Arcavis.Core.Domain.Base;

public static class TenantEntityErrors
{
    #region 1000 - 1099 => Tenant Entity Errors
    public static readonly DomainError InvalidTenant =
        new(1001, "INVALID_TENANT_ID", nameof(TenantEntity));
    #endregion
}
