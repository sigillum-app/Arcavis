using Sigillum.Arcavis.Core.Domain.Common.ExceptionHandling;

namespace Sigillum.Arcavis.Core.Domain.Base;

public abstract class TenantEntity : Entity
{
    public Guid TenantId { get; protected set; }

    protected TenantEntity(Guid id, Guid tenantId)
    {
        SetTenant(tenantId);
    }

    protected TenantEntity()
    { }

    protected void SetTenant(Guid tenantId)
    {
        if (tenantId == Guid.Empty)
            throw new DomainException(TenantEntityErrors.InvalidTenant);

        TenantId = tenantId;
    }
}