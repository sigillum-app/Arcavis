using Sigillum.Arcavis.Core.Domain.Errors;
using Sigillum.Arcavis.Core.Domain.ExceptionHandling;

namespace Sigillum.Arcavis.Core.Domain.Entities.Base;

public abstract class TenantEntity : BaseEntity
{
    public Guid TenantId { get; protected set; }

    protected TenantEntity(Guid id, Guid tenantId) : base(id)
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