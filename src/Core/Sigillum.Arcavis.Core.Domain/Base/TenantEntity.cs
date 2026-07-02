namespace Sigillum.Arcavis.Core.Domain.Base;

public abstract class TenantEntity : Entity
{
    public Guid TenantId { get; protected set; }

    protected TenantEntity()
    { }
}